namespace Ofgem.API.BUS.Applications.Core.Configuration;

public class ApplicationsApiConfiguration
{
    public int ConsentEmailExpiryDays { get; set; }
    public Uri ConsentPortalBaseAddress { get; set; } = null!;
    public Uri ExternalPortalBaseAddress { get; set; } = null!;
}
