namespace Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

/// <summary>
/// Dto containing the Property Owner data required to add a new Application
/// </summary>
public class CreateApplicationRequestPropertyOwnerDetail
{
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? TelephoneNumber { get; set; }
    public string? PropertyOwnerAddressUPRN { get; set; }
    public string? PropertyOwnerAddressLine1 { get; set; }
    public string? PropertyOwnerAddressLine2 { get; set; }
    public string? PropertyOwnerAddressLine3 { get; set; }
    public string? PropertyOwnerAddressCounty { get; set; }
    public string? PropertyOwnerAddressPostcode { get; set; }
    public string? PropertyOwnerAddressCountry { get; set; }
}
