namespace Ofgem.API.BUS.Applications.Domain.Concrete;

/// <summary>
/// PagedResultBase - concrete base entity.
/// </summary>
public abstract class PagedResultBase
{
    /// <summary>
    /// The current page.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The page count.
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    /// The page size.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The row count.
    /// </summary>
    public int RowCount { get; set; }

    /// <summary>
    /// The first row in the list.
    /// </summary>
    public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;

    /// <summary>
    /// The last row in the list.
    /// </summary>
    public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
}