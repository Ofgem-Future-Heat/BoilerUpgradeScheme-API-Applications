namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for the ApplicationSubStatus DB object.
/// </summary>
public partial class ApplicationSubStatus
{
    /// <summary>
    /// A status' unique Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A status code.
    /// </summary>
    public ApplicationSubStatusCode Code { get; set; }

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
    public Guid ApplicationStatusId { get; set; }

    public virtual ApplicationStatus ApplicationStatus { get; set; } = null!;

    public virtual List<ApplicationStatusHistory> ApplicationStatusHistories { get; set; } = new List<ApplicationStatusHistory>();
}