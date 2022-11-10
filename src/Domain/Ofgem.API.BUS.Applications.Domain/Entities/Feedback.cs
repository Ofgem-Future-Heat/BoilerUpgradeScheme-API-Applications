using Ofgem.API.BUS.Applications.Domain.Constants;

namespace Ofgem.API.BUS.Applications.Domain;

/// <summary>
/// Model for Feedback DB object.
/// </summary>
public class Feedback
{
    /// <summary>
    /// Unique internal identifier
    /// </summary>
    public Guid ID { get; set; }

    /// <summary>
    /// ID of the application that the feedback relates to.
    /// </summary>
    public Guid ApplicationID { get; set; }

    /// <summary>
    /// Id of the survey option selected by the Property Owner.
    /// </summary>
    public Guid SurveyOptionId { get; set; }

    /// <summary>
    /// Optional text input user leaves as part of feedback workflow.
    /// </summary>
    public string? FeedBackNarrative { get; set; }

    /// <summary>
    /// The date that the Property Owner provided the feedback.
    /// </summary>
    public DateTime FedbackOn { get; set; }

    /// <summary>
    /// Enum of the application domain the feedback applies to.
    /// </summary>
    public ApplicationDomain AppliesTo { get; set; }

    public virtual Application Application { get; set; } = null!; 

    public virtual SurveyOption SurveyOption { get; set; } = null!; 
}
