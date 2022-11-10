using Microsoft.EntityFrameworkCore;

namespace Ofgem.API.BUS.Applications.Domain;

[Keyless]
public class ApplicationDashboard
{
    public string? AddressLine1 { get; set; }
    public string? Postcode { get; set; }
    public string? IsBeingAudited { get; set; }
    public string? ReferenceNumber { get; set; }

    public string? ApplicationSubStatus { get; set; }

    public string? ApplicationSubStatusCode { get; set; }

    public string? ApplicationAndVoucherStatus { get; set; }

    public string? VoucherSubStatusCode { get; set; }

    public string? ReviewRecommendation { get; set; }
    public string? ConsentState { get; set; }
    public string? ApplicationDate { get; set; }
    public string? RedemptionRequestDate { get; set; }
}
