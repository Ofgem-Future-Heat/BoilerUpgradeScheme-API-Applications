namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for Grant DB object
/// </summary>
public class Grant
{
    /// <summary>
    /// Unique internal identifier.
    /// </summary>
    public Guid ID { get; set; }

    /// <summary>
    /// ID of the tech type.
    /// </summary>
    public Guid? TechTypeID { get; set; }

    /// <summary>
    /// Payment amount of the grant
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Date the grant amount Starts
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Date the grant amount Ends
    /// </summary>
    public DateTime? EndDate { get; set; }

    public virtual TechType? TechType { get; set; }
}
