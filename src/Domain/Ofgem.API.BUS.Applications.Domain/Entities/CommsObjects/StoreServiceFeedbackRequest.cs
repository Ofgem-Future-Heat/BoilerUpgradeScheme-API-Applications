namespace Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

/// <summary>
/// Dto containing the data required to Store Feedback for a BUS service
/// </summary>
public class StoreServiceFeedbackRequest
{
    public Guid ApplicationID { get; set; }
    public string FeedbackNarrative { get; set; } = string.Empty;

    public int SurveyOption { get; set; }
    public string ServiceUsed { get; set; } = string.Empty;
}
