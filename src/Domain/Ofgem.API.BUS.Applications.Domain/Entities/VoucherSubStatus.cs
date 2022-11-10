namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for the VoucherSubStatus DB object.
/// </summary>
public partial class VoucherSubStatus
{
    /// <summary>
    /// A status' unique Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A status code.
    /// </summary>
    public VoucherSubStatusCode Code { get; set; }

    /// <summary>
    /// A display name for the UI.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// A brief description of the status.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// A sort order for the dropdown display in the UI.
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// ID of the parent status.
    /// </summary>
    public Guid VoucherStatusId { get; set; }

    public virtual VoucherStatus VoucherStatus { get; set; } = null!;

    public virtual List<VoucherStatusHistory> VoucherStatusHistories { get; set; } = new List<VoucherStatusHistory>();
}