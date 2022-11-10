namespace Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

/// <summary>
/// Dto containing the data required to update a new Current Contact Id
/// </summary>
public class UpdateContactRequest
{
    public Guid OldContactId { get; set; }
    public Guid NewContactId { get; set; }
}
