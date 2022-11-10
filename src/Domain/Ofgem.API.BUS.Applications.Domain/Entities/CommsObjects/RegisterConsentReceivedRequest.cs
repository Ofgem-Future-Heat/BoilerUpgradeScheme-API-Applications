namespace Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

public class RegisterConsentReceivedRequest
{
    /// <summary>
    /// Username of the user updating the consent request.
    /// </summary>
    public string UpdatedByUsername { get; set; } = string.Empty;
}
