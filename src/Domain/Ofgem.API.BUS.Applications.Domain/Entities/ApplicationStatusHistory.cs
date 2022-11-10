namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for the ApplicationStatusHistory DB object.
/// </summary>
public class ApplicationStatusHistory
{
    /// <summary>
    /// unique identifier of the record
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ID of the sub-status the application has changed to.
    /// </summary>
    public Guid ApplicationSubStatusId { get; set; }

    /// <summary>
    /// Date and time that the Voucher entered the status
    /// </summary>
    public DateTime StartDateTime { get; set; }

    /// <summary>
    /// Date and time that the Voucher exited the status
    /// </summary>
    public DateTime? EndDateTime { get; set; }

    /// <summary>
    /// The application this status change relates to.
    /// </summary>
    public Guid ApplicationId { get; set; }

    public virtual ApplicationSubStatus ApplicationSubStatus { get; set; } = null!;
    public virtual Application Application { get; set; } = null!;
}
