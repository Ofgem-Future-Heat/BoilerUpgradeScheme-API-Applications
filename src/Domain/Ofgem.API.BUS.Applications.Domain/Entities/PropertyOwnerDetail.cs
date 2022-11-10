namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for the PropertyOwnerDetail DB object.
/// </summary>
public class PropertyOwnerDetail
{
    /// <summary>
    /// Unique internal identifier
    /// </summary>
    public Guid ID { get; set; }

    /// <summary>
    /// Full name of the property owner
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Email address of the property owner
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Telephone number of the property owner
    /// </summary>
    public string? TelephoneNumber { get; set; }

    /// <summary>
    /// Indicator to denote if the PO requires Digital Assistance 
    /// </summary>
    public bool? IsAssistedDigital { get; set; }

    /// <summary>
    /// Indicator to denote if the PO requires documents in Welsh
    /// </summary>
    public bool? IsWelshTranslation { get; set; }

    /// <summary>
    /// ID of the property owner's address.
    /// </summary>
    public Guid? PropertyOwnerAddressId { get; set; }

    public virtual PropertyOwnerAddress? PropertyOwnerAddress { get; set; }
}
