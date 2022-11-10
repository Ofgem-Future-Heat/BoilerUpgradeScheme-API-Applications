using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.API.BUS.PropertyConsents.Client.Interfaces;
using Ofgem.API.BUS.PropertyConsents.Domain.Models.CommsObjects;

namespace Ofgem.API.BUS.Applications.Core;

/// <summary>
/// <see cref="IPropertyConsentService"/> implementation
/// </summary>
public class PropertyConsentService : IPropertyConsentService
{
    private readonly IPropertyConsentAPIClient _propertyConsentAPIClient;

    public PropertyConsentService(IPropertyConsentAPIClient propertyConsentAPIClient)
    {
        _propertyConsentAPIClient = propertyConsentAPIClient ?? throw new ArgumentNullException(nameof(propertyConsentAPIClient));
    }

    public async Task<SendConsentEmailResult> SendConsentEmailAsync(SendConsentEmailRequest sendConsentEmailRequest)
    {
        return await _propertyConsentAPIClient.PropertyConsentRequestsClient.SendConsentEmailAsync(sendConsentEmailRequest);
    }
}
