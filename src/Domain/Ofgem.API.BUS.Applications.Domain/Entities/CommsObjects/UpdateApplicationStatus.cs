using static Ofgem.API.BUS.Applications.Domain.ApplicationSubStatus;

namespace Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

/// <summary>
/// Dto containing the data required to update Application status
/// </summary>
public class UpdateApplicationStatus
{
    public Guid ApplicationId { get; set; }
    public ApplicationSubStatusCode StatusCode { get; set; }
}
