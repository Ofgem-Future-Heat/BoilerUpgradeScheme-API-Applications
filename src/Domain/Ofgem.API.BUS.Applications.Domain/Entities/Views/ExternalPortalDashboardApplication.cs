using Microsoft.EntityFrameworkCore;
using Ofgem.API.BUS.Applications.Domain.Entities.Enums;
using static Ofgem.API.BUS.Applications.Domain.ApplicationStatus;
using static Ofgem.API.BUS.Applications.Domain.VoucherStatus;

namespace Ofgem.API.BUS.Applications.Domain.Entities.Views;

/// <summary>
/// Model for the external dashboard application DB object.
/// </summary>
[Keyless]
public class ExternalPortalDashboardApplication
{
    public Guid ApplicationId { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public Guid BusinessAccountId { get; set; }
    public DateTime? InstallerReplyByDate { get; set; }
    public DateTime ApplicationCreatedDate { get; set; }
    public string? PropertyOwnerFullName { get; set; }
    public string InstallationAddressLine1 { get; set; } = string.Empty;
    public string? InstallationAddressLine2 { get; set; }
    public string? InstallationAddressLine3 { get; set; }
    public string? InstallationAddressCounty { get; set; }
    public string InstallationAddressPostcode { get; set; } = string.Empty;
    public Guid ApplicationStatusId { get; set; }
    public ApplicationStatusCode ApplicationStatusCode { get; set; }
    public Guid? VoucherStatusId { get; set; }
    public VoucherStatusCode? VoucherStatusCode { get; set; }
    public DateTime? ConsentExpiryDate { get; set; }
    public DateTime? VoucherExpiryDate { get; set; }
    public ConsentState ConsentState { get; set; }
}
