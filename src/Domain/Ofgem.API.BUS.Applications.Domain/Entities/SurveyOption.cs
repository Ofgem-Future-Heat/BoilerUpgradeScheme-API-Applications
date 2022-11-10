namespace Ofgem.API.BUS.Applications.Domain;

public class SurveyOption
{
    public Guid ID { get; set; }

    public string FeedbackOption { get; set; } = string.Empty;

    public int Order { get; set; }
}