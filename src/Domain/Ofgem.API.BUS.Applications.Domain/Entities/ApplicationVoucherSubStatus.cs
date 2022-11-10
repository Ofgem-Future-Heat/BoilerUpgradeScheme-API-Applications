namespace Ofgem.API.BUS.Applications.Domain.Entities;

public class ApplicationVoucherSubStatus
{
    /// <summary>
    /// A status' unique Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A status code.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// A display name for the UI.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// A brief description of the status.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// A sort order for the dropdown display in the UI.
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// Is Checked is used by the UI process the user interacion with the dataset.
    /// </summary>
    public bool IsChecked { get; set; }
}
