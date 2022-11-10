using Ofgem.API.BUS.Applications.Domain.Interfaces;

namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for Consent Request DB object.
/// </summary>
public class ConsentRequest : ICreateModify
{
    /// <summary>
    /// Unique internal identifier
    /// </summary>
    public Guid ID { get; set; }

    /// <summary>
    /// ID of the application that the consent relates to.
    /// </summary>
    public Guid ApplicationID { get; set; }

    /// <summary>
    /// DateTime that the PO Consent was issued
    /// </summary>
    public DateTime? ConsentIssuedDate { get; set; }

    /// <summary>
    /// The date that the Property Owner must provide Consent by
    /// </summary>
    public DateTime? ConsentExpiryDate { get; set; }

    /// <summary>
    /// DateTime that the PO provided consent
    /// </summary>
    public DateTime? ConsentReceivedDate { get; set; }

    public virtual Application Application { get; set; } = null!;

    /// <summary>
    /// Date and time the record was created
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Email address of the user who created the record
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// DateAndTime the record was last updated
    /// </summary>
    public DateTime? LastUpdatedDate { get; set; }

    /// <summary>
    /// Email address of the user who last updated the record
    /// </summary>
    public string? LastUpdatedBy { get; set; }
}
