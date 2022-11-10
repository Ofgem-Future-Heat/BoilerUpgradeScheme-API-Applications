namespace Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

/// <summary>
/// Dto containing the Installation Address data required to add a new Application
/// </summary>
public class CreateApplicationRequestInstallationAddress
{
    public string Line1 { get; set; } = null!;
    public string? Line2 { get; set; }
    public string? Line3 { get; set; }
    public string? County { get; set; }
    public string Postcode { get; set; } = null!;
    public string? UPRN { get; set; } = null!;
    public string? CountryCode { get; set; }
}
