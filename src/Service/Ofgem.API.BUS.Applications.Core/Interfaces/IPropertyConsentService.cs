using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;

namespace Ofgem.API.BUS.Applications.Core.Interfaces;

/// <summary>
/// Interface for Property Consent API functions
/// </summary>
public interface IPropertyConsentService
{
    /// <summary>
    /// Sends a request to the Property Consent API for an email to be raised using the provided settings
    /// </summary>
    /// <param name="sendConsentEmailRequest"></param>
    /// <returns></returns>
    public Task<SendConsentEmailResult> SendConsentEmailAsync(SendConsentEmailRequest sendConsentEmailRequest);
}
