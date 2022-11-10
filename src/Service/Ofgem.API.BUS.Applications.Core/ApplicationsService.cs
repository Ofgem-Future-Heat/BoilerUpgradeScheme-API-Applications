using AutoMapper;
using Ofgem.API.BUS.Applications.Core.Configuration;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Constants;
using Ofgem.API.BUS.Applications.Domain.Entities;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;
using Ofgem.API.BUS.Applications.Domain.Entities.Enums;
using Ofgem.API.BUS.Applications.Domain.Entities.Views;
using Ofgem.API.BUS.Applications.Domain.Request;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using static Ofgem.API.BUS.Applications.Domain.ApplicationSubStatus;

namespace Ofgem.API.BUS.Applications.Core;

/// <summary>
/// <see cref="IApplicationsService"/> implementation
/// </summary>
public class ApplicationsService : IApplicationsService
{
    private readonly IMapper _mapper;
    private readonly IApplicationsProvider _applicationsProvider;
    private readonly IPropertyConsentService _propertyConsentService;
    private readonly IBusinessAccountsService _businessAccountsService;
    private readonly ApplicationsApiConfiguration _applicationsApiConfiguration;

    public ApplicationsService(
        IMapper mapper,
        IApplicationsProvider applicationsProvider,
        IBusinessAccountsService businessAccountsService,
        IPropertyConsentService propertyConsentService,
        ApplicationsApiConfiguration applicationsApiConfiguration)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _applicationsProvider = applicationsProvider ?? throw new ArgumentNullException(nameof(applicationsProvider));
        _businessAccountsService = businessAccountsService ?? throw new ArgumentNullException(nameof(businessAccountsService));
        _propertyConsentService = propertyConsentService ?? throw new ArgumentNullException(nameof(propertyConsentService));
        _applicationsApiConfiguration = applicationsApiConfiguration ?? throw new ArgumentNullException(nameof(applicationsApiConfiguration));
    }

    public async Task<Voucher> AddVoucherAsync(AddVoucherRequest addVoucherRequest)
    {
        await ValidateAddVoucherRequestAsync(addVoucherRequest);

        var mappedVoucher = _mapper.Map<Voucher>(addVoucherRequest);

        var grant = await _applicationsProvider.GetGrantOrDefaultByTechTypeIdAsync(addVoucherRequest.TechTypeId);

        if (grant == null)
        {
            throw new ResourceNotFoundException($"Could not find matching grant for Tech Type ID {addVoucherRequest.TechTypeId}");
        }

        mappedVoucher.GrantId = grant.ID;

        return await _applicationsProvider.AddVoucherAsync(mappedVoucher);
    }

    public async Task<Application> CreateApplicationAsync(CreateApplicationRequest request)
    {
        await ValidateCreateApplicationRequestAsync(request);
        var otherApplicationsForUprn = new List<Application>();

        if (!string.IsNullOrEmpty(request.InstallationAddress?.UPRN))
        {
            otherApplicationsForUprn = await _applicationsProvider.GetApplicationsByInstallationAddressUprnAsync(request.InstallationAddress.UPRN);
            bool duplicateApplicationExists = CheckForDuplicateApplications(request.BusinessAccountID, otherApplicationsForUprn);

            if (duplicateApplicationExists)
            {
                throw new BadRequestException("The business already has an application at this installation address");
            }
        }

        var mappedApplication = _mapper.Map<Application>(request);
        mappedApplication = await AddNewApplicationReferenceNumberAsync(mappedApplication);

        bool consentedApplicationExists = false;
        if (otherApplicationsForUprn is not null && otherApplicationsForUprn.Any())
        {
            var consentReceivedApplications = otherApplicationsForUprn.Where(app => app.ConsentRequests != null
                                                                                    && app.ConsentRequests.Any(x => x.ConsentReceivedDate is not null));

            if (consentReceivedApplications.Any())
            {
                consentedApplicationExists = true;
            }
        }

        mappedApplication.SubStatusId = consentedApplicationExists ?
                                        StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.CNTRW] :
                                        StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.SUB];

        return await _applicationsProvider.AddApplicationAsync(mappedApplication);
    }

    public async Task<bool> CheckDuplicateApplicationAsync(string uprn, Guid businessAccountId)
    {

        var otherApplicationsForUprn = await _applicationsProvider.GetApplicationsByInstallationAddressUprnAsync(uprn);
        bool duplicateApplicationExists = CheckForDuplicateApplications(businessAccountId, otherApplicationsForUprn);
        bool consentedApplicationExists = false;

        if (duplicateApplicationExists && otherApplicationsForUprn is not null && otherApplicationsForUprn.Any())
        {
            //Only consider an application duplicate if it is consented
            var consentReceivedApplications = otherApplicationsForUprn.Where(app => app.ConsentRequests != null
                                                                                    && app.ConsentRequests.Any(x => x.ConsentReceivedDate is not null));

            consentedApplicationExists = consentReceivedApplications.Any();
        }

        return consentedApplicationExists;
    }

    private async Task<Application> AddNewApplicationReferenceNumberAsync(Application application)
    {
        var applicationReferenceNumber = await _applicationsProvider.GetNewApplicationNumberAsync();
        application.ReferenceNumber = $"GID{applicationReferenceNumber}";
        return application;
    }

    public async Task<bool> CheckForActiveConsentByApplicationIdAsync(Guid applicationId)
    {
        var application = await _applicationsProvider.GetApplicationOrDefaultWithConsentObjectsByIdAsync(applicationId);

        if (application == null)
        {
            throw new ResourceNotFoundException($"Application ID {applicationId} not found");
        }
        if (string.IsNullOrEmpty(application.InstallationAddress?.UPRN))
        {
            throw new ResourceNotFoundException($"Installation address UPRN not found for Application ID {applicationId}");
        }

        var otherUprnApplications = await GetApplicationsByUprnAsync(application.InstallationAddress.UPRN);
        var otherApplicationConsented = CheckApplicationsForActiveConsent(otherUprnApplications);

        return otherApplicationConsented;
    }

    public async Task<SendConsentEmailResult> SendConsentEmailAsync(Guid applicationId, string createdByUser)
    {
        var application = await _applicationsProvider.GetApplicationOrDefaultWithConsentObjectsByIdAsync(applicationId);

        if (application == null)
        {
            throw new ResourceNotFoundException($"Application ID {applicationId} not found");
        }

        if (!string.IsNullOrWhiteSpace(application.InstallationAddress?.UPRN))
        {
            var otherUprnApplications = await GetApplicationsByUprnAsync(application.InstallationAddress.UPRN);
            var otherApplicationConsented = CheckApplicationsForActiveConsent(otherUprnApplications);

            if (otherApplicationConsented)
            {
                throw new BadRequestException("This installation address already has an application with an active consent.");
            }
        }

        SendConsentEmailRequest request = await GenerateSendConsentEmailRequestAsync(application);
        var result = await _propertyConsentService.SendConsentEmailAsync(request);

        if (result != null && result.IsSuccess && result.ConsentRequestId != Guid.Empty && result.ConsentTokenExpires.HasValue)
        {
            await _applicationsProvider.UpsertConsentRequestAsync(applicationId, new ConsentRequest()
            {
                ID = result.ConsentRequestId,
                ConsentExpiryDate = result.ConsentTokenExpires.Value,
                ConsentIssuedDate = DateTime.UtcNow,
                ApplicationID = application.ID,
                CreatedBy = createdByUser
            });

            return result;
        }

        throw new BadRequestException("Cannot issue consent because there is a problem with the property owner's email");

    }

    private async Task<SendConsentEmailRequest> GenerateSendConsentEmailRequestAsync(Application application)
    {
        if (application.PropertyOwnerDetail?.Email == null)
        {
            throw new BadRequestException(ApplicationsExceptionMessages.NoPropertyOwnerEmailAddress);
        }

        application.InstallationAddress ??= new();
        application.TechType ??= new();

        Guid consentRequestId = Guid.NewGuid();
        if (application.ConsentRequests is not null && application.ConsentRequests.Any())
        {
            var mostRecent = application.ConsentRequests.OrderBy(x => x.ConsentIssuedDate).First();
            consentRequestId = mostRecent.ID;
        }

        return new SendConsentEmailRequest()
        {
            ConsentRequestId = consentRequestId,
            InstallerName = await TryGetInstallerNameByBusinessAccountIDAsync(application.BusinessAccountId),
            EmailAddress = application.PropertyOwnerDetail.Email,
            ConsentRequestExpiryDays = _applicationsApiConfiguration.ConsentEmailExpiryDays,
            ApplicationReferenceNumber = application.ReferenceNumber,
            PropertyOwnerPortalBaseURL = _applicationsApiConfiguration.ConsentPortalBaseAddress,
            TechnologyType = application.TechType.TechTypeDescription ?? string.Empty,
            InstallationAddressLine1 = application.InstallationAddress.AddressLine1 ?? string.Empty,
            InstallationAddressLine2 = application.InstallationAddress.AddressLine2 ?? string.Empty,
            InstallationAddressLine3 = application.InstallationAddress.AddressLine3 ?? string.Empty,
            InstallationAddressCounty = application.InstallationAddress.County ?? string.Empty,
            InstallationAddressPostcode = application.InstallationAddress.Postcode ?? string.Empty
        };
    }

#pragma warning disable S4457 // Parameter validation in "async"/"await" methods should be wrapped. Method is performing parameter validation.
    private async Task ValidateCreateApplicationRequestAsync(CreateApplicationRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        if (request.BusinessAccountID == Guid.Empty) throw new BadRequestException(ApplicationsExceptionMessages.NoBusinessAccount);
        _ = await TryGetInstallerNameByBusinessAccountIDAsync(request.BusinessAccountID);

        // installation address
        if (request.InstallationAddress == null) throw new BadRequestException(ApplicationsExceptionMessages.NoInstallationAddress);
        if (string.IsNullOrWhiteSpace(request.InstallationAddress.Line1)) throw new BadRequestException(ApplicationsExceptionMessages.NoInstallationAddressLine1);
        if (string.IsNullOrWhiteSpace(request.InstallationAddress.Postcode)) throw new BadRequestException(ApplicationsExceptionMessages.NoInstallationAddressPostcode);

        // Quote amount
        if (request.QuoteAmount > 999999.99M) throw new BadRequestException(ApplicationsExceptionMessages.QuoteAmountMaxValue);
    }
#pragma warning restore S4457 // Parameter validation in "async"/"await" methods should be wrapped

    private async Task ValidateAddVoucherRequestAsync(AddVoucherRequest addVoucherRequest)
    {
        if (addVoucherRequest == null) throw new ArgumentNullException(nameof(addVoucherRequest));
        if (addVoucherRequest.ApplicationID == Guid.Empty) throw new BadRequestException(ApplicationsExceptionMessages.EmptyApplicationId);

        await DoValidateAddVoucherRequestAsync(addVoucherRequest);
    }

    private async Task DoValidateAddVoucherRequestAsync(AddVoucherRequest addVoucherRequest)
    {
        Application? application;
        try
        {
            application = await _applicationsProvider.GetApplicationOrDefaultByIdAsync(addVoucherRequest.ApplicationID);
        }
        catch (Exception ex)
        {
            throw new BadRequestException($"Unable to resolve Application ID. Error message: {ex.Message}");
        }
        if (application == null)
        {
            throw new ResourceNotFoundException($"Unable to resolve Application ID {addVoucherRequest.ApplicationID}");
        }

        TechType? techType;
        try
        {
            techType = await _applicationsProvider.GetTechTypeOrDefaultByIdAsync(addVoucherRequest.TechTypeId);
        }
        catch (Exception ex)
        {
            throw new BadRequestException($"Unable to resolve Tech Type ID. Error message: {ex.Message}");
        }
        if (techType == null)
        {
            throw new ResourceNotFoundException($"Unable to resolve Tech Type ID {addVoucherRequest.TechTypeId}");
        }
    }

    public async Task<GetConsentRequestDetailsResult> GetConsentRequestDetailsAsync(Guid consentRequestId)
    {
        var result = new GetConsentRequestDetailsResult()
        {
            IsSuccess = false
        };

        var consentRequest = await _applicationsProvider.GetConsentRequestOrDefaultByIdAsync(consentRequestId);
        if (consentRequest == null || consentRequest.ConsentExpiryDate == null)
        {
            return result;
        }

        var application = await _applicationsProvider.GetApplicationWithConsentObjectsByIdAsync(consentRequest.ApplicationID);
        var businessAccountName = await _businessAccountsService.GetBusinessAccountNameByIdAsync(application.BusinessAccountId);
        var businessAccountEmailAddress = await _businessAccountsService.GetBusinessAccountEmailByInstallerIdAsync(application.CurrentContactId ?? application.SubmitterId);

        var consentRequestSummary = new ConsentRequestSummary
        {
            InstallerName = businessAccountName,
            InstallerEmailId = businessAccountEmailAddress,
            OwnerEmailId = application.PropertyOwnerDetail!.Email!,
            OwnerFullName = application.PropertyOwnerDetail.FullName!,
            ApplicationReferenceNumber = application.ReferenceNumber,
            InstallationAddressLine1 = application.InstallationAddress!.AddressLine1!,
            InstallationAddressLine2 = application.InstallationAddress.AddressLine2!,
            InstallationAddressLine3 = application.InstallationAddress.AddressLine3,
            InstallationAddressCounty = application.InstallationAddress.County!,
            InstallationAddressPostcode = application.InstallationAddress.Postcode!,
            InstallationAddressUprn = application.InstallationAddress.UPRN!,
            QuoteAmount = application.QuoteAmount!.Value,
            ServiceLevelAgreementDate = consentRequest.ConsentExpiryDate.Value,
            TechnologyType = application.TechType!.TechTypeDescription,
            HasConsented = consentRequest.ConsentReceivedDate,
            ExpiryDate = consentRequest.ConsentExpiryDate.Value
        };

        result.IsSuccess = true;
        result.ConsentRequestSummary = consentRequestSummary;
        return result;

    }

    public async Task<string> GetBusinessEmailAddressByInstallerId(Guid installerId)
    {
        var businessAccountEmailAddress = await _businessAccountsService.GetBusinessAccountEmailByInstallerIdAsync(installerId);
        if (businessAccountEmailAddress == null)
        {
            throw new ResourceNotFoundException($"Consent request {installerId} not found");
        }

        return businessAccountEmailAddress;
    }

    public async Task RegisterConsentAsync(Guid consentRequestId, string updatedByUser)
    {
        var consentRequest = await _applicationsProvider.GetConsentRequestOrDefaultByIdAsync(consentRequestId);
        if (consentRequest == null)
        {
            throw new ResourceNotFoundException($"Consent request {consentRequestId} not found");
        }

        await _applicationsProvider.RegisterConsentReceivedByIdAsync(consentRequestId, updatedByUser);
    }

    private async Task<string> TryGetInstallerNameByBusinessAccountIDAsync(Guid businessAccountID)
    {
        string installerName;
        try
        {
            installerName = await _businessAccountsService.GetBusinessAccountNameByIdAsync(businessAccountID);
        }
        catch (HttpRequestException ex)
        {
            if (ex.Message.Contains("Bad Request"))
            {
                throw new BadRequestException("Unable to resolve Business Account ID");
            }

            throw;
        }

        if (string.IsNullOrEmpty(installerName))
        {
            throw new ResourceNotFoundException($"Unable to resolve Business Account ID {businessAccountID}");
        }

        return installerName;
    }

    public async Task<PagedResult<ApplicationDashboard>> GetPagedApplications(int page = 1, int pageSize = 20,
        string sortBy = "ApplicationDate", bool orderByDescending = true,
        string? filterApplicationStatusBy = "", string? filterVoucherStatusBy = "",
        string? isBeingAudited = null, string? searchBy = null)
    {
        /* Parse the application statuses passed into the operation*/
        List<string>? listOfFilterApplicationStatusBy = null;
        if (!string.IsNullOrEmpty(filterApplicationStatusBy))
        {
            listOfFilterApplicationStatusBy = filterApplicationStatusBy.Replace(@"\", "").Split(",").AsEnumerable().ToList();
        }

        /* Parse the voucher statuses passed into the operation*/
        List<string>? listOfFilterVoucherStatusBy = null;
        if (!string.IsNullOrEmpty(filterVoucherStatusBy))
        {
            listOfFilterVoucherStatusBy = filterVoucherStatusBy.Replace(@"\", "").Split(",").AsEnumerable().ToList();
        }

        /* Stop the search and degrade gracefully */
        if (!string.IsNullOrEmpty(searchBy) && searchBy.Replace(@"\", "").Length < 3)
        {
            searchBy = string.Empty;
        }

        return await _applicationsProvider.GetPagedApplications(page, pageSize, sortBy, orderByDescending,
            listOfFilterApplicationStatusBy, listOfFilterVoucherStatusBy,
            isBeingAudited, searchBy);
    }

    public IEnumerable<ExternalPortalDashboardApplication> GetExternalDashboardApplicationsByBusinessAccountId(Guid businessAccountId, string? searchBy, IEnumerable<Guid>? statusIds, string? consentState)
    {
        ConsentState? consentStateEnum;
        try
        {
            consentStateEnum = !string.IsNullOrEmpty(consentState) ? Enum.Parse<ConsentState>(consentState) : null;
        }
        catch (ArgumentException)
        {
            consentStateEnum = null;
        }

        return _applicationsProvider.GetExternalDashboardApplicationsByBusinessAccountId(businessAccountId, searchBy, statusIds, consentStateEnum);
    }

    public async Task<IEnumerable<Application>> GetApplicationsByUprnAsync(string uprn)
    {
        return await _applicationsProvider.GetApplicationsByInstallationAddressUprnAsync(uprn);
    }

    public async Task<IEnumerable<TechType>> GetTechTypeListAsync()
    {
        return await _applicationsProvider.GetTechTypeListAsync();
    }

    public async Task<bool> UpdateApplicationAsync(Application application)
    {
        return await _applicationsProvider.UpdateApplicationAsync(application);
    }

    public async Task<bool> UpdateVoucherAsync(Voucher voucher)
    {
        return await _applicationsProvider.UpdateVoucherAsync(voucher);
    }

    public bool CheckForDuplicateApplications(Guid businessAccountId, IEnumerable<Application> applications)
    {
        var invalidExpiredStatusCodes = new ApplicationSubStatusCode[]
        {
            ApplicationSubStatusCode.CNTRD,
            ApplicationSubStatusCode.WITHDRAWN,
            ApplicationSubStatusCode.VEXPD,
            ApplicationSubStatusCode.REJECTED,
            ApplicationSubStatusCode.RPEND
        };

        var isDuplicate = applications.Any(a => a.BusinessAccountId == businessAccountId && (a.SubStatus != null && !invalidExpiredStatusCodes.Contains(a.SubStatus.Code)));

        return isDuplicate;
    }

    public bool CheckApplicationsForActiveConsent(IEnumerable<Application> applications)
    {
        if (applications == null || !applications.Any())
        {
            throw new ArgumentNullException(nameof(applications));
        }

        var invalidExpiredStatusCodes = new ApplicationSubStatusCode[]
        {
            ApplicationSubStatusCode.CNTRD,
            ApplicationSubStatusCode.WITHDRAWN,
            ApplicationSubStatusCode.VEXPD,
            ApplicationSubStatusCode.REJECTED,
            ApplicationSubStatusCode.RPEND
        };

        var isAnyApplicationConsented = applications.Any(app => (app.ConsentRequests != null && app.ConsentRequests.Any(cons => cons.ConsentReceivedDate != null))
                                                         && (app.SubStatus != null && !invalidExpiredStatusCodes.Contains(app.SubStatus.Code)));

        return isAnyApplicationConsented;
    }

    public async Task<Application> GetApplicationByIdAsync(Guid id)
    {
        var application = await _applicationsProvider.GetApplicationOrDefaultByIdAsync(id);

        if (application == null)
        {
            throw new BadRequestException($"Could not find application for ID {id}");
        }

        return application;
    }

    public async Task<Application> GetApplicationByReferenceNumber(string referenceNumber)
    {
        var application = await _applicationsProvider.GetApplicationOrDefaultByReferenceNumberAsync(referenceNumber);

        return application == null
            ? throw new ResourceNotFoundException($"Could not find application for reference number {referenceNumber}")
            : application;
    }
    public async Task<IEnumerable<ApplicationVoucherSubStatus>> GetApplicationVoucherSubStatusesListAsync()
    {
        IEnumerable<ApplicationVoucherSubStatus>? appLitsOfStatuses = await _applicationsProvider.GetApplicationVoucherSubStatusesListAsync();

        return !appLitsOfStatuses.Any()
            ? throw new BadRequestException($"Could not find list of application & voucher statuses")
            : appLitsOfStatuses;
    }
    public async Task<IEnumerable<ApplicationSubStatus>> GetApplicationSubStatusesListAsync()
    {
        IEnumerable<ApplicationSubStatus>? appLitsOfStatuses = await _applicationsProvider.GetApplicationSubStatusesListAsync();

        return !appLitsOfStatuses.Any()
            ? throw new BadRequestException($"Could not find list of application statuses")
            : appLitsOfStatuses;
    }
    public async Task<IEnumerable<VoucherSubStatus>> GetVoucherSubStatusesListAsync()
    {
        IEnumerable<VoucherSubStatus>? appLitsOfStatuses = await _applicationsProvider.GetVoucherSubStatusesListAsync();

        return !appLitsOfStatuses.Any()
            ? throw new BadRequestException($"Could not find list of voucher statuses")
            : appLitsOfStatuses;
    }

    public async Task<List<string>> UpdateApplicationStatusAsync(Guid applicationId, Guid applicationStatusId)
    {
        return await _applicationsProvider.UpdateApplicationStatusAsync(applicationId, applicationStatusId);
    }

    public async Task<List<string>> UpdateVoucherStatusAsync(Guid voucherId, Guid voucherStatusId)
    {
        return await _applicationsProvider.UpdateVoucherStatusAsync(voucherId, voucherStatusId);
    }
    public async Task<List<string>> UpdateEpcReferenceAsync(Guid applicationId)
    {
        return await _applicationsProvider.UpdateEpcReferenceAsync(applicationId);
    }

    public async Task<IEnumerable<Grant>> GetGrantsListAsync()
    {
        return await _applicationsProvider.GetGrantsListAsync();
    }

    public async Task<StoreServiceFeedbackResult> StoreServiceFeedbackAsync(StoreServiceFeedbackRequest serviceFeedback)
    {
        var result = await _applicationsProvider.StoreServiceFeedbackAsync(
            serviceFeedback.ApplicationID, serviceFeedback.FeedbackNarrative, serviceFeedback.SurveyOption, serviceFeedback.ServiceUsed
            );

        return new StoreServiceFeedbackResult { IsSuccess = result };
    }

    public async Task<TechType?> GetTechTypeByIdAsync(Guid techTypeId)
    {
        return await _applicationsProvider.GetTechTypeOrDefaultByIdAsync(techTypeId);
    }

    public async Task<bool> UpdateContactAsync(UpdateContactRequest updateContactRequest)
    {
        return await _applicationsProvider.UpdateContactAsync(updateContactRequest.OldContactId, updateContactRequest.NewContactId);
    }

    public bool HasLiveApplicationsAgainstExternalUserId(Guid externalUserId)
    {
        return _applicationsProvider.HasLiveApplicationsAgainstExternalUserId(externalUserId);
    }
}
