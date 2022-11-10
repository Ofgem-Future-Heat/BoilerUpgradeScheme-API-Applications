namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for the InstallationAddress DB object.
/// </summary>
public class PropertyOwnerAddress
{
    /// <summary>
    /// Unique internal identifier
    /// </summary>
    public Guid ID { get; set; }

    /// <summary>
    /// Unique property reference number of the PO Address
    /// </summary>
    public string? UPRN { get; set; }

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
    /// county for the PO address
    /// </summary>
    public string? County { get; set; }

    /// <summary>
    /// postcode for the Installation address
    /// </summary>
    public string? Postcode { get; set; }

    /// <summary>
    /// Country for the property owner's resident address
    /// </summary>
    public string? Country { get; set; }
}
