namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for the TechType DB object.
/// </summary>
public class TechType
{
    /// <summary>
    /// Unique internal identifier
    /// </summary>
    public Guid ID { get; set; }

    public Guid? MCSTechTypeId { get; set; }

    /// <summary>
    /// Description of the tech type
    /// </summary>
    public string TechTypeDescription { get; set; } = null!;

    /// <summary>
    ///  Expiry interval in months - used to determine the voucher expiry date
    /// </summary>
    public int ExpiryIntervalMonths { get; set; }
}
