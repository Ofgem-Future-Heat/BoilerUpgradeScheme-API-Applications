namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for the VoucherStatusHistory DB object.
/// </summary>
public class VoucherStatusHistory
{
    /// <summary>
    /// unique identifier of the record
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ID of the sub-status the voucher has changed to.
    /// </summary>
    public Guid VoucherSubStatusId { get; set; }

    /// <summary>
    /// Date and time that the Voucher entered the status
    /// </summary>
    public DateTime StartDateTime { get; set; }

    /// <summary>
    /// Date and time that the Voucher exited the status
    /// </summary>
    public DateTime? EndDateTime { get; set; }

    /// <summary>
    /// ID of the voucher related to this status history.
    /// </summary>
    public Guid VoucherId { get; set; }

    public virtual VoucherSubStatus VoucherSubStatus { get; set; } = null!;
    public virtual Voucher Voucher { get; set; } = null!;
}
