namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
///  Model for the Product DB object.
/// </summary>
public class Product
{
    /// <summary>
    /// Unique internal identifier
    /// </summary>
    public Guid Id { get; set; }

    public string MCSProductName { get; set; } = string.Empty; 

    public string MCSModelNumber { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    /// <summary>
    /// Tech type of the Product
    /// </summary>
    public string TechnologyId { get; set; } = string.Empty;

    /// <summary>
    /// Tech type description of the Product
    /// </summary>
    public string TechnologyDescription { get; set; } = string.Empty;

    public string ProductTypeId { get; set; } = string.Empty;

    public string ProductTypeDescription { get; set; } = string.Empty;

    /// <summary>
    /// SCOP value of the product
    /// </summary>
    public int SCOP35ToSCOP65 { get; set; }

    /// <summary>
    /// Product certification date
    /// </summary>
    public DateTime CertifiedFrom { get; set; }

    /// <summary>
    /// Product certification expiry date
    /// </summary>
    public DateTime CertifiedTo { get; set; }
}
