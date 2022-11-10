namespace Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

public class RequestConsentEmailRequest
{
    /// <summary>
    /// Username of the user creating the consent request.
    /// </summary>
    public string CreatedByUsername { get; set; } = string.Empty;
}
