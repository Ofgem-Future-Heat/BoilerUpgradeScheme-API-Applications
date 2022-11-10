using Notify.Exceptions;
using Notify.Interfaces;
using Ofgem.API.BUS.Applications.Core.Configuration;
using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Concrete;

namespace Ofgem.API.BUS.Applications.Core;

/// <summary>
/// Implements <see cref="IEmailService"/> using Gov Notify.
/// </summary>
public class NotifyEmailService : IEmailService
{
    private readonly IAsyncNotificationClient _govNotifyClient;
    private readonly GovNotifyConfiguration _govNotifyConfiguration;
    private readonly ApplicationsApiConfiguration _applicationsApiConfiguration;
    private readonly IBusinessAccountsService _businessAccountsService;
    private readonly IApplicationsService _applicationsService;

    public NotifyEmailService(IAsyncNotificationClient govNotifyClient,
                              GovNotifyConfiguration configuration,
                              IBusinessAccountsService businessAccountsService,
                              IApplicationsService applicationsService,
                              ApplicationsApiConfiguration applicationsApiConfiguration)
    {
        _govNotifyClient = govNotifyClient ?? throw new ArgumentNullException(nameof(govNotifyClient));
        _govNotifyConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _businessAccountsService = businessAccountsService ?? throw new ArgumentNullException(nameof(businessAccountsService));
        _applicationsService = applicationsService ?? throw new ArgumentNullException(nameof(applicationsService));
        _applicationsApiConfiguration = applicationsApiConfiguration ?? throw new ArgumentNullException(nameof(applicationsApiConfiguration));
    }

    public async Task<SendEmailResult> SendInstallerPostApplicationEmailAsync(Application application)
    {
        var result = new SendEmailResult();
        var isWelshTranslation = application.PropertyOwnerDetail?.IsWelshTranslation ?? false;
        var isAssistedDigital = application.PropertyOwnerDetail?.IsAssistedDigital ?? false;
        var isWelshOrAd = isWelshTranslation || isAssistedDigital;
        var isSelfBuild = application.IsSelfBuild ?? false;
        var epcExemptions = application.IsLoftCavityExempt ?? false;

        try
        {
            var templateId = GetInstallerPostApplicationEmailTemplateId(isSelfBuild, epcExemptions);
            var recipientEmail = await _businessAccountsService.GetBusinessAccountEmailByInstallerIdAsync(application.SubmitterId);
            var personalisationObject = await GetInstallerPostApplicationEmailPersonalisationObject(application, isWelshOrAd);

            var emailResult = await _govNotifyClient.SendEmailAsync(recipientEmail,
                                                                    templateId.ToString(),
                                                                    personalisationObject,
                                                                    emailReplyToId: _govNotifyConfiguration.InstallerPostApplicationEmailReplyToId.ToString());
            result.IsSuccess = emailResult != null;
        }
        catch (NotifyClientException ex)
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Error received from Gov Notify - {ex.HResult} - {ex.Message}";
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    private async Task<Dictionary<string, dynamic>> GetInstallerPostApplicationEmailPersonalisationObject(Application application, bool isWelshOrAd)
    {
        var installerName = await _businessAccountsService.GetBusinessAccountNameByIdAsync(application.BusinessAccountId);
        var techType = application.TechType ?? await _applicationsService.GetTechTypeByIdAsync(application.TechTypeId!.Value);
        var propertyOwnerEmail = !isWelshOrAd ? application.PropertyOwnerDetail?.Email ?? string.Empty : string.Empty;

        var multiLineAddress = string.Empty;
        if (!string.IsNullOrWhiteSpace(application.InstallationAddress?.AddressLine1)) { multiLineAddress += $"{application.InstallationAddress.AddressLine1}\n"; }
        if (!string.IsNullOrWhiteSpace(application.InstallationAddress?.AddressLine2)) { multiLineAddress += $"{application.InstallationAddress.AddressLine2}\n"; }
        if (!string.IsNullOrWhiteSpace(application.InstallationAddress?.AddressLine3)) { multiLineAddress += $"{application.InstallationAddress.AddressLine3}\n"; }
        if (!string.IsNullOrWhiteSpace(application.InstallationAddress?.County)) { multiLineAddress += $"{application.InstallationAddress.County}\n"; }
        if (!string.IsNullOrWhiteSpace(application.InstallationAddress?.Postcode)) { multiLineAddress += $"{application.InstallationAddress.Postcode}\n"; }
        multiLineAddress = multiLineAddress.TrimEnd(Environment.NewLine.ToCharArray());

        var personalisationObject = new Dictionary<string, dynamic>
        {
            {"Postcode", application.InstallationAddress?.Postcode ?? string.Empty },
            {"InstallerName", installerName },
            {"TechnologyType", techType?.TechTypeDescription ?? string.Empty },
            {"MultilineAddress", multiLineAddress },
            {"ApplicationReferenceNumber", application.ReferenceNumber },
            {"ADOrWelsh", isWelshOrAd ? "yes" : "no" },
            {"NotADOrWelsh", !isWelshOrAd ? "yes" : "no" },
            {"PropertyOwnerEmail", propertyOwnerEmail },
            {"ApplicationDashboardUrl", _applicationsApiConfiguration.ExternalPortalBaseAddress }
        };

        return personalisationObject;
    }

    private Guid GetInstallerPostApplicationEmailTemplateId(bool isElgibileSelfBuild, bool hasEpcExemptions)
    {
        Guid templateId;

        if (isElgibileSelfBuild)
        {
            templateId = _govNotifyConfiguration.InstallerPostApplicationTemplates.EligibleNewBuild;
        }
        else if (hasEpcExemptions)
        {
            templateId = _govNotifyConfiguration.InstallerPostApplicationTemplates.EpcExemptions;
        }
        else
        {
            templateId = _govNotifyConfiguration.InstallerPostApplicationTemplates.NoEvidenceRequired;
        }

        return templateId;
    }
}
