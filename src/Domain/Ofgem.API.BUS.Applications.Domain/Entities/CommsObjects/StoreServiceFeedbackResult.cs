namespace Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

/// <summary>
/// Dto containing the success status from an attempt to store feedback from one of the bus services.
/// </summary>
public class StoreServiceFeedbackResult
{
    public bool IsSuccess { get; set; }
}
