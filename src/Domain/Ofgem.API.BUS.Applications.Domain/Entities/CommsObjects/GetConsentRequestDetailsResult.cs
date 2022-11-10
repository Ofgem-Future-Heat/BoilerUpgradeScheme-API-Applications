using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;

namespace Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

public class GetConsentRequestDetailsResult
{
    public bool IsSuccess { get; set; }
    public ConsentRequestSummary? ConsentRequestSummary { get; set; }
}
