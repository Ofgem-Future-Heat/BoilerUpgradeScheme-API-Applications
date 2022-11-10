namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for the InstallationAddress DB object.
/// </summary>
public class InstallationAddress
{
    /// <summary>
    /// Unique internal identifier
    /// </summary>
    public Guid ID { get; set; }

    /// <summary>
    /// building name / no plus street name
    /// </summary>
    public string? AddressLine1 { get; set; }

    /// <summary>
    /// second line of address
    /// </summary>
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// Town
    /// </summary>
    public string? AddressLine3 { get; set; }

    /// <summary>
    /// county for the Installation address
    /// </summary>
    public string? County { get; set; }

    /// <summary>
    /// postcode for the Installation address
    /// </summary>
    public string? Postcode { get; set; }

    /// <summary>
    /// Unique property reference number of the Installation Address
    /// </summary>
    public string? UPRN { get; set; }

    /// <summary>
    /// The country code (obtained from os places), if found.
    /// </summary>
    public string? CountryCode { get; set; }

    /// <summary>
    /// Indicator that denotes if the property is in a rural locations
    /// </summary>
    public string? IsRural { get; set; }

    /// <summary>
    /// Indicator that denotes if the property is on the mains gas grid
    /// </summary>
    public bool? IsGasGrid { get; set; }
}
