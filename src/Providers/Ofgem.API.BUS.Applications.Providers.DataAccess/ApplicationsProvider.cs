using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Constants;
using Ofgem.API.BUS.Applications.Domain.Entities;
using Ofgem.API.BUS.Applications.Domain.Entities.Enums;
using Ofgem.API.BUS.Applications.Domain.Entities.Views;
using Ofgem.API.BUS.Applications.Domain.Request;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Helpers;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using static Ofgem.API.BUS.Applications.Domain.ApplicationSubStatus;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess;

/// <summary>
/// IApplicationsProvider implementation
/// </summary>
public class ApplicationsProvider : IApplicationsProvider
{
    private readonly ApplicationsDBContext _context;

    public ApplicationsProvider(ApplicationsDBContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Application> AddApplicationAsync(Application application)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        _context.Applications.Add(application);

        if (application.SubStatusId != null)
        {
            await UpsertApplicationStatusHistory(application.ID, application.SubStatusId.Value);
        }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return application;
    }

    public async Task UpsertConsentRequestAsync(Guid applicationId, ConsentRequest consentRequest)
    {
        if (consentRequest.ApplicationID != applicationId)
        {
            throw new ArgumentException("Application IDs must match", nameof(applicationId));
        }

        await DoUpsertConsentRequestAsync(applicationId, consentRequest);
    }

    private async Task DoUpsertConsentRequestAsync(Guid applicationId, ConsentRequest consentRequest)
    {
        if (!await _context.Applications.AnyAsync(x => x.ID == applicationId))
        {
            throw new ResourceNotFoundException(nameof(applicationId));
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        var existingConsentRequestThisID = await _context.ConsentRequests.SingleOrDefaultAsync(x => x.ID == consentRequest.ID);
        if (existingConsentRequestThisID is null)
        {
            _context.ConsentRequests.Add(consentRequest);
        }
        else
        {
            existingConsentRequestThisID.ConsentIssuedDate = consentRequest.ConsentIssuedDate;
            existingConsentRequestThisID.ConsentExpiryDate = consentRequest.ConsentExpiryDate;
        }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task<Application> GetApplicationByIdAsync(Guid id)
    {
        return await _context.Applications.SingleAsync(x => x.ID == id);
    }

    public async Task<Application> GetApplicationWithConsentObjectsByIdAsync(Guid id)
    {
        return await _context.Applications
            .Include(x => x.InstallationAddress).Include(x => x.TechType)
            .Include(x => x.PropertyOwnerDetail).Include(x => x.ConsentRequests)
            .Include(x => x.Voucher)
            .SingleAsync(x => x.ID == id);
    }

    public async Task<TechType> GetTechTypeByIdAsync(Guid id)
    {
        return await _context.TechTypes.SingleAsync(x => x.ID == id);
    }

    public async Task<Grant> GetGrantByIdAsync(Guid id)
    {
        return await _context.Grants.SingleAsync(x => x.ID == id);
    }

    public async Task<Voucher> AddVoucherAsync(Voucher voucher)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        _context.Vouchers.Add(voucher);

        if (voucher.VoucherSubStatusID != null)
        {
            await UpsertVoucherStatusHistory(voucher.ID, voucher.VoucherSubStatusID.Value);
        }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return voucher;
    }

    public async Task<ConsentRequest> GetConsentRequestByIdAsync(Guid id)
    {
        return await _context.ConsentRequests.SingleAsync(x => x.ID == id);
    }

    public async Task RegisterConsentReceivedByIdAsync(Guid id, string updatedByUser)
    {
        var consentRequest = await _context.ConsentRequests.SingleAsync(x => x.ID == id);

        await using var transaction = await _context.Database.BeginTransactionAsync();
        consentRequest.ConsentReceivedDate = DateTime.UtcNow;
        consentRequest.LastUpdatedBy = updatedByUser;
        await _context.SaveChangesAsync();

        await transaction.CommitAsync();
    }

    public async Task<Application?> GetApplicationOrDefaultByIdAsync(Guid id)
    {
        return await _context.Applications.Include(x => x.InstallationAddress)
                                          .Include(x => x.PropertyOwnerDetail)
                                          .Include(x => x.PropertyOwnerDetail!.PropertyOwnerAddress)
                                          .Include(x => x.Epc)
                                          .Include(x => x.TechType)
                                          .Include(x => x.SubStatus)
                                          .Include(x => x.ConsentRequests)
                                          .Include(x => x.Voucher)
                                          .SingleOrDefaultAsync(x => x.ID == id);
    }

    public async Task<Application?> GetApplicationOrDefaultByReferenceNumberAsync(string referenceNumber)
    {
        return await _context.Applications.Include(x => x.InstallationAddress)
                                          .Include(x => x.PropertyOwnerDetail)
                                          .Include(x => x.PropertyOwnerDetail!.PropertyOwnerAddress)
                                          .Include(x => x.Epc)
                                          .Include(x => x.TechType)
                                          .Include(x => x.SubStatus)
                                          .Include(x => x.SubStatus!.ApplicationStatus)
                                          .Include(x => x.ConsentRequests)
                                          .Include(x => x.ApplicationStatusHistories)
                                          .Include(x => x.Voucher)
                                          .Include(x => x.Voucher!.VoucherSubStatus)
                                          .Include(x => x.Voucher!.VoucherSubStatus.VoucherStatus)
                                          .Include(x => x.Voucher!.Grant)
                                          .SingleOrDefaultAsync(x => x.ReferenceNumber == referenceNumber);
    }

    public async Task<Application?> GetApplicationOrDefaultWithConsentObjectsByIdAsync(Guid id)
    {
        return await _context.Applications
            .Include(x => x.InstallationAddress).Include(x => x.TechType).Include(x => x.PropertyOwnerDetail)
            .Include(x => x.ConsentRequests).Include(x => x.Voucher)
            .SingleOrDefaultAsync(x => x.ID == id);
    }

    public async Task<TechType?> GetTechTypeOrDefaultByIdAsync(Guid id)
    {
        return await _context.TechTypes.SingleOrDefaultAsync(x => x.ID == id);
    }

    public async Task<Grant?> GetGrantOrDefaultByIdAsync(Guid id)
    {
        return await _context.Grants.SingleOrDefaultAsync(x => x.ID == id);
    }

    public async Task<Grant?> GetGrantOrDefaultByTechTypeIdAsync(Guid techTypeId)
    {
        return await _context.Grants.SingleOrDefaultAsync(x => x.TechTypeID == techTypeId);
    }

    public async Task<ConsentRequest?> GetConsentRequestOrDefaultByIdAsync(Guid id)
    {
        return await _context.ConsentRequests.SingleOrDefaultAsync(x => x.ID == id);
    }

    public async Task<PagedResult<ApplicationDashboard>> GetPagedApplications(
        int page = 1,
        int pageSize = 20,
        string sortBy = "ApplicationDate",
        bool orderByDescending = true,
        List<string>? filterApplicationStatusBy = null,
        List<string>? filterVoucherStatusBy = null,
        string? isBeingAudited = null,
        string? searchBy = null)
    {
        if (string.IsNullOrEmpty(sortBy))
        {
            throw new ArgumentException($"'{nameof(sortBy)}' cannot be null or empty.", nameof(sortBy));
        }

        /* This is now using a view, therefore, all datatypes are strings. */
        IQueryable<ApplicationDashboard>? applicationVouchers = _context.ApplicationDashboards.AsQueryable();
        /* Do we have any data to display. */
        if (!applicationVouchers.Any())
        {
            return new();
        }

        /* Filtering by status code and audit flag. */
        applicationVouchers = FilterByStatusCodeAndAuditFlag(isBeingAudited ?? "0", filterApplicationStatusBy, filterVoucherStatusBy, applicationVouchers);

        /* Search by string */
        applicationVouchers = ApplicationSearchBy(searchBy ?? "", applicationVouchers);

        /* Order data set by Ascending or Descending. */
        IOrderedQueryable<ApplicationDashboard> sortedDataSubSet = ApplicationSortBy(sortBy, orderByDescending, applicationVouchers);

        /* Return paged results ... */
        return await sortedDataSubSet.GetPagedAsync(page, pageSize).ConfigureAwait(false);
    }

    private IQueryable<ApplicationDashboard> FilterByStatusCodeAndAuditFlag(
        string isBeingAudited,
        ICollection<string>? filterApplicationStatusBy,
        ICollection<string>? filterVoucherStatusBy,
        IQueryable<ApplicationDashboard> applicationVouchers)
    {
        bool auditBy = isBeingAudited == "1";

        if (filterApplicationStatusBy != null && filterVoucherStatusBy != null)
        {
            if (auditBy)
            {
                return applicationVouchers.Where(a => (filterApplicationStatusBy.Contains(a.ApplicationSubStatusCode!)
                                                        && a.VoucherSubStatusCode == "NAN"
                                                        && a.IsBeingAudited == "1")
                                                || filterVoucherStatusBy.Contains(a.VoucherSubStatusCode!)
                                                && a.IsBeingAudited == "1");
            }

            return applicationVouchers.Where(a => (filterApplicationStatusBy.Contains(a.ApplicationSubStatusCode!) && a.VoucherSubStatusCode == "NAN")
                                                || filterVoucherStatusBy.Contains(a.VoucherSubStatusCode!));
        }

        if (filterApplicationStatusBy != null)
        {
            if (auditBy)
            {
                return applicationVouchers.Where(a => (filterApplicationStatusBy.Contains(a.ApplicationSubStatusCode!)
                && a.VoucherSubStatusCode == "NAN")
                && a.IsBeingAudited == "1");
            }

            return applicationVouchers.Where(a => filterApplicationStatusBy.Contains(a.ApplicationSubStatusCode!)
            && a.VoucherSubStatusCode == "NAN");
        }

        if (filterVoucherStatusBy != null)
        {
            if (auditBy)
            {
                return applicationVouchers.Where(a => filterVoucherStatusBy.Contains(a.VoucherSubStatusCode!)
                && a.IsBeingAudited == "1");
            }

            return applicationVouchers.Where(a => filterVoucherStatusBy.Contains(a.VoucherSubStatusCode!));
        }

        if (auditBy && filterApplicationStatusBy == null && filterVoucherStatusBy == null)
        {
            return applicationVouchers.Where(a => a.IsBeingAudited == "1");

        }

        return applicationVouchers;
    }

    private IQueryable<ApplicationDashboard> ApplicationSearchBy(string searchBy, IQueryable<ApplicationDashboard> applicationVouchers)
    {
        if (!string.IsNullOrEmpty(searchBy) && searchBy.Length >= 3)
        {
            applicationVouchers = applicationVouchers.Where(c => (!string.IsNullOrEmpty(c.Postcode) && c.Postcode.Contains(searchBy))
                                                                 || (!string.IsNullOrEmpty(c.ReferenceNumber) && c.ReferenceNumber.Contains(searchBy)));
        }

        return applicationVouchers;
    }

    private IOrderedQueryable<ApplicationDashboard> ApplicationSortBy(string sortBy, bool orderByDescending,
        IQueryable<ApplicationDashboard> applicationVouchers)
    {
        IOrderedQueryable<ApplicationDashboard> sortedDataSubSet = sortBy switch
        {
            "Postcode" => orderByDescending
                ? applicationVouchers.OrderByDescending(x => x.Postcode)
                : applicationVouchers.OrderBy(x => x.Postcode),

            "ReferenceNumber" => orderByDescending
                ? applicationVouchers.OrderByDescending(x => x.ReferenceNumber)
                : applicationVouchers.OrderBy(x => x.ReferenceNumber),

            "ApplicationSubStatus" => orderByDescending
                ? applicationVouchers.OrderByDescending(x => x.ApplicationAndVoucherStatus!.ToUpper())
                : applicationVouchers.OrderBy(x => x.ApplicationAndVoucherStatus!.ToUpper()),

            "ReviewRecommendation" => orderByDescending
                ? applicationVouchers.OrderByDescending(x => x.ReviewRecommendation)
                : applicationVouchers.OrderBy(x => x.ReviewRecommendation),

            "ConsentState" => orderByDescending
                ? applicationVouchers.OrderByDescending(x => x.ConsentState)
                : applicationVouchers.OrderBy(x => x.ConsentState),

            "RedemptionRequestDate" => orderByDescending
                ? applicationVouchers.OrderByDescending(x => x.RedemptionRequestDate)
                : applicationVouchers.OrderBy(x => x.RedemptionRequestDate),

            "ApplicationDate" => orderByDescending
                ? applicationVouchers.OrderByDescending(x => x.ApplicationDate)
                : applicationVouchers.OrderBy(x => x.ApplicationDate),

            _ => applicationVouchers.OrderByDescending(x => x.ApplicationDate),
        };

        return sortedDataSubSet;
    }

    public async Task<IEnumerable<ApplicationVoucherSubStatus>> GetApplicationVoucherSubStatusesListAsync()
    {
        List<ApplicationSubStatus>? listOfAppStatus = await _context.ApplicationSubStatuses.AsNoTracking().ToListAsync().ConfigureAwait(false);

        List<VoucherSubStatus>? listOfVoucherStatus = await _context.VoucherSubStatuses.AsNoTracking().ToListAsync().ConfigureAwait(false);

        List<ApplicationVoucherSubStatus> returnListOfStatus = new();

        returnListOfStatus.AddRange(from l in listOfAppStatus
                                    .OrderBy(p => p.SortOrder)
                                    let a = new ApplicationVoucherSubStatus
                                    {
                                        SortOrder = l.SortOrder,
                                        DisplayName = l.DisplayName,
                                        Description = l.Description,
                                        Code = l.Code.ToString(),
                                        Id = l.Id
                                    }
                                    select a);

        returnListOfStatus.AddRange(from l in listOfVoucherStatus.OrderBy(p => p.SortOrder)
                                    let a = new ApplicationVoucherSubStatus
                                    {
                                        SortOrder = l.SortOrder,
                                        DisplayName = l.DisplayName,
                                        Description = l.Description,
                                        Code = l.Code.ToString(),
                                        Id = l.Id
                                    }
                                    select a);
        return returnListOfStatus;
    }

    public async Task<IEnumerable<ApplicationSubStatus>> GetApplicationSubStatusesListAsync()
    {
        List<ApplicationSubStatus>? listOfAppStatus = await _context.ApplicationSubStatuses.AsNoTracking().ToListAsync().ConfigureAwait(false);

        List<ApplicationSubStatus> returnListOfStatus = new();

        returnListOfStatus.AddRange(from l in listOfAppStatus
                                    .OrderBy(p => p.SortOrder)
                                    let a = new ApplicationSubStatus
                                    {
                                        SortOrder = l.SortOrder,
                                        DisplayName = l.DisplayName,
                                        Description = l.Description,
                                        ApplicationStatusId = l.ApplicationStatusId,
                                        Code = l.Code,
                                        Id = l.Id
                                    }
                                    select a);

        return returnListOfStatus;
    }
    public async Task<IEnumerable<VoucherSubStatus>> GetVoucherSubStatusesListAsync()
    {
        List<VoucherSubStatus>? listOfVoucherStatus = await _context.VoucherSubStatuses.AsNoTracking().ToListAsync().ConfigureAwait(false);

        List<VoucherSubStatus> returnListOfStatus = new();

        returnListOfStatus.AddRange(from l in listOfVoucherStatus.OrderBy(p => p.SortOrder)
                                    let a = new VoucherSubStatus
                                    {
                                        SortOrder = l.SortOrder,
                                        DisplayName = l.DisplayName,
                                        Description = l.Description,
                                        VoucherStatusId = l.VoucherStatusId,
                                        Code = l.Code,
                                        Id = l.Id
                                    }
                                    select a);
        return returnListOfStatus;
    }

    public async Task<IEnumerable<TechType>> GetTechTypeListAsync()
    {
        return await _context.TechTypes.AsNoTracking().ToListAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<Grant>> GetGrantsListAsync()
    {
        return await _context.Grants.AsNoTracking().ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<Application>> GetApplicationsByInstallationAddressUprnAsync(string uprn)
    {
        var applications = _context.Applications.Include(x => x.SubStatus)
                                                .Include(x => x.ConsentRequests)
                                                .Include(x => x.PropertyOwnerDetail)
                                                .Include(x => x.Voucher)
                                                .Where(x => x.InstallationAddress!.UPRN == uprn);

        return await applications.ToListAsync().ConfigureAwait(false);
    }

    public IEnumerable<ExternalPortalDashboardApplication> GetExternalDashboardApplicationsByBusinessAccountId(Guid businessAccountId,
                                                                                                                    string? searchBy,
                                                                                                                    IEnumerable<Guid>? statusIds,
                                                                                                                    ConsentState? consentState)
    {
        var dashboardApplications = _context.ExternalPortalDashboardApplications
            .FromSqlInterpolated($"EXECUTE [dbo].[uspGetPreSortedExternalDashboard] {businessAccountId}")
            .AsEnumerable()
            .Where(application =>
                SearchByFilter(application, searchBy)
                && StatusIdFilter(application, statusIds)
                && ConsentStateFilter(application, consentState));

        return dashboardApplications.ToList();
    }

    public async Task<int> GetNewApplicationNumberAsync()
    {
        var globalSetting = new GlobalSetting();
        var generatedById = Guid.NewGuid();
        globalSetting.GeneratedByID = generatedById;

        await using var transaction = await _context.Database.BeginTransactionAsync();
        _context.GlobalSettings.Add(globalSetting);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return globalSetting.NextApplicationReferenceNumber;
    }

    public async Task<bool> UpdateApplicationAsync(Application application)
    {
        await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync().ConfigureAwait(false);

        Application? foundApplication = await _context.Applications.FindAsync(application.ID).ConfigureAwait(false);
        if (foundApplication == null)
        {
            throw new ResourceNotFoundException($"Application not found for {foundApplication}");
        }

        if (application.InstallationAddress != null)
        {
            InstallationAddress? foundInstallation = await _context.InstallationAddresses.FindAsync(application.InstallationAddress.ID)
                .ConfigureAwait(false);
            if (foundInstallation == null)
            {
                Guid id = _context.InstallationAddresses.Add(application.InstallationAddress).Entity.ID;
                application.InstallationAddressID = id;
            }
            else
            {
                _context.Entry(foundInstallation).CurrentValues.SetValues(application.InstallationAddress);
            }
        }

        if (application.PropertyOwnerDetail?.PropertyOwnerAddress != null)
        {
            PropertyOwnerAddress? foundPropertyOwnerAddress = await _context.PropertyOwnerAddresses
                .FindAsync(application.PropertyOwnerDetail.PropertyOwnerAddress.ID).ConfigureAwait(false);
            if (foundPropertyOwnerAddress == null)
            {
                Guid id = _context.PropertyOwnerAddresses.Add(application.PropertyOwnerDetail.PropertyOwnerAddress)
                    .Entity.ID;
                application.PropertyOwnerDetail.PropertyOwnerAddressId = id;
            }
            else
            {
                _context.Entry(foundPropertyOwnerAddress).CurrentValues
                    .SetValues(application.PropertyOwnerDetail.PropertyOwnerAddress);
            }
        }

        if (application.PropertyOwnerDetail != null)
        {
            PropertyOwnerDetail? foundPropertyOwnerDetail = await _context.PropertyOwnerDetails
                .FindAsync(application.PropertyOwnerDetail.ID).ConfigureAwait(false);
            if (foundPropertyOwnerDetail == null)
            {
                Guid id = _context.PropertyOwnerDetails.Add(application.PropertyOwnerDetail).Entity.ID;
                application.PropertyOwnerDetailId = id;
            }
            else
            {
                _context.Entry(foundPropertyOwnerDetail).CurrentValues.SetValues(application.PropertyOwnerDetail);
            }
        }

        if (application.Epc != null)
        {
            Epc? foundEpcs = await _context.EPCs.FindAsync(application.Epc.ID).ConfigureAwait(false);
            if (foundEpcs == null)
            {
                Guid id = _context.EPCs.Add(application.Epc).Entity.ID;
                application.EpcId = id;
            }
            else
            {
                _context.Entry(foundEpcs).CurrentValues.SetValues(application.Epc);
            }
        }

        if (application.Voucher != null)
        {
            Voucher? foundVoucher = await _context.Vouchers.FindAsync(application.Voucher.ID).ConfigureAwait(false);
            if (foundVoucher == null)
            {
                _context.Vouchers.Add(application.Voucher);
            }
            else
            {
                _context.Entry(foundVoucher).CurrentValues.SetValues(application.Voucher);
            }
        }

        if (application.ConsentRequests.Any())
        {
            IQueryable<ConsentRequest> foundConsentRequests = _context.ConsentRequests.Where(p => p.ApplicationID == application.ID);

            if (!foundConsentRequests.Any())
            {
                foreach (var appConsentReq in application.ConsentRequests)
                {
                    _context.ConsentRequests.Add(appConsentReq);
                }
            }
            else
            {
                foreach (var appConsentReq in application.ConsentRequests)
                {
                    foreach (var consentRequest in foundConsentRequests)
                    {
                        _context.Entry(consentRequest).CurrentValues.SetValues(appConsentReq);
                    }
                }
            }
        }

        _context.Entry(foundApplication).CurrentValues.SetValues(application);

        int recordsUpdated = await _context.SaveChangesAsync().ConfigureAwait(false);

        if (recordsUpdated > 0 && application.SubStatusId != null)
        {
            await UpsertApplicationStatusHistory(application.ID, application.SubStatusId.Value).ConfigureAwait(false);
        }

        await transaction.CommitAsync().ConfigureAwait(false);

        return recordsUpdated > 0;
    }

    public async Task<bool> UpdateVoucherAsync(Voucher voucher)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        _context.Vouchers.Update(voucher);
        int recordsUpdated = await _context.SaveChangesAsync();

        if (recordsUpdated > 0 && voucher.VoucherSubStatusID != null)
        {
            await UpsertVoucherStatusHistory(voucher.ID, voucher.VoucherSubStatusID.Value).ConfigureAwait(false);
        }

        await transaction.CommitAsync();

        return recordsUpdated > 0;
    }

    public async Task<bool> UpsertApplicationStatusHistory(Guid applicationId, Guid applicationSubStatusId)
    {
        var previousApplicationHistories = await _context.ApplicationStatusHistories.Where(h => h.ApplicationId == applicationId && h.EndDateTime == null)
                                                                                    .OrderBy(h => h.StartDateTime)
                                                                                    .ToListAsync();

        if (previousApplicationHistories != null && previousApplicationHistories.Any())
        {
            if (previousApplicationHistories.Any(a => a.ApplicationSubStatusId == applicationSubStatusId))
            {
                return true;
            }

            foreach (var previousApplicationHistory in previousApplicationHistories)
            {
                var updateHistory = previousApplicationHistories.First(h => h.Id == previousApplicationHistory.Id);
                updateHistory.EndDateTime = DateTime.UtcNow;
                _context.Update(updateHistory);
            }
        }

        var currentStatusHistoryChange = new ApplicationStatusHistory
        {
            ApplicationId = applicationId,
            StartDateTime = DateTime.UtcNow,
            ApplicationSubStatusId = applicationSubStatusId
        };

        await _context.ApplicationStatusHistories.AddAsync(currentStatusHistoryChange);
        var changeCount = await _context.SaveChangesAsync();

        return changeCount > 0;
    }

    public async Task<bool> UpsertVoucherStatusHistory(Guid voucherId, Guid voucherSubStatusId)
    {
        var previousVoucherHistories = await _context.VoucherStatusHistories.Where(h => h.VoucherId == voucherId && h.EndDateTime == null)
                                                                            .OrderBy(h => h.StartDateTime)
                                                                            .ToListAsync();

        if (previousVoucherHistories != null && previousVoucherHistories.Any())
        {
            if (previousVoucherHistories.Any(a => a.VoucherSubStatusId == voucherSubStatusId))
            {
                return true;
            }

            foreach (var previousVoucherHistory in previousVoucherHistories)
            {
                var updateHistory = previousVoucherHistories.First(h => h.Id == previousVoucherHistory.Id);
                updateHistory.EndDateTime = DateTime.UtcNow;
                _context.Update(updateHistory);
            }
        }

        var currentStatusHistoryChange = new VoucherStatusHistory
        {
            VoucherId = voucherId,
            StartDateTime = DateTime.UtcNow,
            VoucherSubStatusId = voucherSubStatusId
        };

        await _context.VoucherStatusHistories.AddAsync(currentStatusHistoryChange);
        var changeCount = await _context.SaveChangesAsync();

        return changeCount > 0;
    }

    public async Task<List<string>> UpdateApplicationStatusAsync(Guid applicationId, Guid applicationStatusId)
    {
        List<string> vs = new();

        var application = _context.Applications
            .Include(x => x.SubStatus)
            .Include(x => x.Voucher)
            .Include(x => x.TechType)
            .SingleOrDefault(a => a.ID == applicationId);

        if (application == null)
        {
            vs.Add($"Failed to find Application record for {applicationId}");
            return vs;
        }

        await CheckAndSetVoucherIssueAndExpiry(application, applicationStatusId);

        await using var transaction = await _context.Database.BeginTransactionAsync();

        application.SubStatusId = applicationStatusId;

        _context.Applications.Update(application);
        int recordsUpdated = await _context.SaveChangesAsync();

        // Only expecting a max of two record updates (Application and Voucher)
        if (recordsUpdated < 1 || recordsUpdated > 2)
        {
            vs.Add($"Failed to update Application to DB {applicationId}");
            return vs;
        }

        try
        {
            await UpsertApplicationStatusHistory(applicationId, applicationStatusId).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            vs.Add($"Failed to update Status for Application {applicationId} {ex.Message}");
        }

        await transaction.CommitAsync();

        return vs;
    }

    public async Task<List<string>> UpdateVoucherStatusAsync(Guid voucherId, Guid voucherStatusId)
    {
        List<string> vs = new();

        var voucher = _context.Vouchers.SingleOrDefault(a => a.ID == voucherId);
        if (voucher == null)
        {
            vs.Add($"Failed to find Voucher record for {voucherId}");
            return vs;
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        voucher.VoucherSubStatusID = voucherStatusId;

        _context.Vouchers.Update(voucher);
        int recordsUpdated = await _context.SaveChangesAsync();

        if (recordsUpdated != 1)
        {
            vs.Add($"Failed to update Voucher to DB {voucherId}");
            return vs;
        }

        try
        {
            await UpsertVoucherStatusHistory(voucherId, voucherStatusId).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            vs.Add($"Failed to update Status {voucherId} {ex.Message}");
        }

        await transaction.CommitAsync();

        return vs;
    }

    public async Task<List<string>> UpdateEpcReferenceAsync(Guid applicationId)
    {
        List<string> vs = new();

        var application = _context.Applications.SingleOrDefault(a => a.ID == applicationId);
        if (application == null)
        {
            vs.Add($"Failed to find Application record for {applicationId}");
            return vs;
        }

        var epc = _context.EPCs.SingleOrDefault(a => a.ID == application.EpcId);
        if (epc == null)
        {
            vs.Add($"Failed to find EPC record for {application.EpcId}");
            return vs;
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        application.EpcId = null;
        _context.Applications.Update(application);

        _context.EPCs.Remove(epc);

        int recordsUpdated = await _context.SaveChangesAsync();

        await transaction.CommitAsync();

        if (recordsUpdated != 2)
        {
            vs.Add($"Failed to update Application to DB {applicationId}");
            return vs;
        }

        return vs;
    }

    public async Task<bool> StoreServiceFeedbackAsync(Guid applicationId, string feedbackNarrative, int surveyOption, string serviceUsed)
    {
        var surveyOptionSelected = await _context.SurveyOptions.Where(so => so.Order == surveyOption).Select(s => s.ID).FirstAsync();

        var serviceUsedByUser = ApplicationDomain.Unknown;

        switch (serviceUsed)
        {
            case "Consent":
                serviceUsedByUser = ApplicationDomain.ConsentPortal;
                break;
            case "External":
                serviceUsedByUser = ApplicationDomain.ExternalPortal;
                break;
        }

        var feedback = new Feedback
        {
            ApplicationID = applicationId,
            SurveyOptionId = surveyOptionSelected,
            FeedBackNarrative = feedbackNarrative,
            FedbackOn = DateTime.Now,
            AppliesTo = serviceUsedByUser
        };


        await using var transaction = await _context.Database.BeginTransactionAsync();
        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return true;
    }


    public async Task<bool> UpdateContactAsync(Guid oldContactId, Guid newContactId)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        var recordsToUpdate = await _context.Applications
            .Where(a => a.CurrentContactId == oldContactId || (!a.CurrentContactId.HasValue && a.SubmitterId == oldContactId))
            .ToListAsync();

        recordsToUpdate.ForEach(x =>
        {
            x.CurrentContactId = newContactId;
        });

        int recordsUpdated = await _context.SaveChangesAsync();

        await transaction.CommitAsync();

        return recordsUpdated > 0;
    }


    private async Task CheckAndSetVoucherIssueAndExpiry(Application application, Guid applicationStatusId)
    {
        // drop out if voucher wasn't set to issued or application has not yet been consented.
        if (StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.VISSD] == applicationStatusId
            && application.Voucher != null
            && application.SubStatus?.Code != ApplicationSubStatusCode.VISSD)
        {
            var techType = await GetTechTypeByIdAsync(application.TechTypeId!.Value);

            if (techType == null)
            {
                throw new ResourceNotFoundException($"Could not find matching tech type for Tech Type ID {application.TechTypeId}");
            }

            var issuedDate = DateTime.UtcNow;
            var expiryDate = issuedDate.AddMonths(techType.ExpiryIntervalMonths);

            application.Voucher!.IssuedDate = issuedDate;
            application.Voucher!.ExpiryDate = new DateTime(expiryDate.Year, expiryDate.Month, expiryDate.Day, 23, 59, 59, 59);
        }
    }

    public bool HasLiveApplicationsAgainstExternalUserId(Guid externalUserId)
    {
        var liveApplicationStatusIds = new List<Guid> {
            StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.SUB],
            StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.INRW],
            StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.WITH],
            StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.QC],
            StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.DA],
            StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.VPEND],
            StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.VQUED],
            StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.VEXPD],
            StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.CNTRW],
            StatusMappings.ApplicationSubStatus[ApplicationSubStatusCode.RPEND],
        };

        var liveVoucherStatusIds = new List<Guid> {
            StatusMappings.VoucherSubStatus[VoucherSubStatus.VoucherSubStatusCode.SUB],
            StatusMappings.VoucherSubStatus[VoucherSubStatus.VoucherSubStatusCode.REDREV],
            StatusMappings.VoucherSubStatus[VoucherSubStatus.VoucherSubStatusCode.WITHIN],
            StatusMappings.VoucherSubStatus[VoucherSubStatus.VoucherSubStatusCode.QC],
            StatusMappings.VoucherSubStatus[VoucherSubStatus.VoucherSubStatusCode.DA],
            StatusMappings.VoucherSubStatus[VoucherSubStatus.VoucherSubStatusCode.REDAPP],
            StatusMappings.VoucherSubStatus[VoucherSubStatus.VoucherSubStatusCode.SENTPAY],
            StatusMappings.VoucherSubStatus[VoucherSubStatus.VoucherSubStatusCode.REJPEND],
            StatusMappings.VoucherSubStatus[VoucherSubStatus.VoucherSubStatusCode.PAYSUS],
        };

        IEnumerable<Application> liveApplicationsAgainstUser = _context.Applications.Include(x => x.Voucher)
            .Where(x => x.CurrentContactId != null ? x.CurrentContactId == externalUserId : x.SubmitterId == externalUserId)
            .Where(x => x.Voucher == null || x.Voucher.VoucherSubStatus == null ? liveApplicationStatusIds.Contains(x.SubStatus!.Id) : liveVoucherStatusIds.Contains(x.Voucher.VoucherSubStatus.Id))
            .ToList();

        if (liveApplicationsAgainstUser.Any()) return true;

        return false;
    }

    private bool ConsentStateFilter(ExternalPortalDashboardApplication application, ConsentState? consentState)
    {
        if (consentState != null)
        {
            return consentState == application.ConsentState;
        }

        return true;
    }

    private bool StatusIdFilter(ExternalPortalDashboardApplication application, IEnumerable<Guid>? statusIds)
    {
        if (statusIds != null && statusIds!.Any())
        {
            return (application.VoucherStatusId != null && statusIds.Contains(application.VoucherStatusId.Value) || statusIds.Contains(application.ApplicationStatusId));
        }

        return true;
    }

    private bool SearchByFilter(ExternalPortalDashboardApplication application, string? searchBy)
    {
        if (!string.IsNullOrWhiteSpace(searchBy))
        {
            return application.ReferenceNumber.Contains(searchBy, StringComparison.OrdinalIgnoreCase)
                   || application.InstallationAddressLine1.Contains(searchBy, StringComparison.OrdinalIgnoreCase)
                   || (!string.IsNullOrWhiteSpace(application.InstallationAddressLine2) && application.InstallationAddressLine2!.Contains(searchBy, StringComparison.OrdinalIgnoreCase))
                   || (!string.IsNullOrWhiteSpace(application.InstallationAddressLine3) && application.InstallationAddressLine3!.Contains(searchBy, StringComparison.OrdinalIgnoreCase))
                   || (!string.IsNullOrWhiteSpace(application.InstallationAddressCounty) && application.InstallationAddressCounty!.Contains(searchBy, StringComparison.OrdinalIgnoreCase))
                   || application.InstallationAddressPostcode.Contains(searchBy, StringComparison.OrdinalIgnoreCase)
                   || (!string.IsNullOrWhiteSpace(application.PropertyOwnerFullName) && application.PropertyOwnerFullName!.Contains(searchBy, StringComparison.OrdinalIgnoreCase));
        }

        return true;
    }
}