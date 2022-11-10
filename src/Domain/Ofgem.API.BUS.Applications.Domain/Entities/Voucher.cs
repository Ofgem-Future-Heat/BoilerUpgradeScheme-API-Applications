namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for Voucher DB object.
/// </summary>
public class Voucher
{
    /// <summary>
    /// Unique internal identifier
    /// </summary>
    public Guid ID { get; set; }

    /// <summary>
    /// Issued date of the Application voucher
    /// </summary>
    public DateTime? IssuedDate { get; set; }

    /// <summary>
    /// Expiry date of the Application voucher
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Date at which the installer requested to redeem the voucher
    /// </summary>
    public DateTime? RedemptionRequestDate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// Used to record the Redeem recommended decision (Pass or Fail) for the QC reviewer
    /// </summary>
    public bool? QCRecommendation { get; set; }

    /// <summary>
    /// Used to record the Redeem recommended decision (Pass or Fail) for the DA reviewer
    /// </summary>
    public bool? DARecommendation { get; set; }

    /// <summary>
    /// ID of the grant that the voucher is for.
    /// </summary>
    public Guid GrantId { get; set; }

    /// <summary>
    /// ID of the application
    /// </summary>
    public Guid ApplicationID { get; set; }

    /// <summary>
    /// ID of the voucher sub status
    /// </summary>
    public Guid? VoucherSubStatusID { get; set; }

    public virtual Application Application { get; set; } = null!;
    public virtual List<VoucherStatusHistory> VoucherStatusHistories { get; set; } = new List<VoucherStatusHistory>();
    public virtual VoucherSubStatus VoucherSubStatus { get; set; } = null!;
    public virtual Grant Grant { get; set; } = null!;
}
