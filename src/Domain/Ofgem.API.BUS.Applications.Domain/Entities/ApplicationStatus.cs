namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for the ApplicationStatus DB object.
/// </summary>
public partial class ApplicationStatus
{
    /// <summary>
    /// A status' unique Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A status code.
    /// </summary>
    public ApplicationStatusCode Code { get; set; }

    /// <summary>
    /// A brief description of the status.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// The status's display name as shown in the UI
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// A sort order for the dropdown display in the UI.
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// A list of Sub statuses (Child Statuses to a parent Status).
    /// </summary>
    public virtual List<ApplicationSubStatus>? ApplicationSubStatus { get; set; }
}