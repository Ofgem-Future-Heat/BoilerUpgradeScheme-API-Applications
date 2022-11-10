using Ofgem.API.BUS.Applications.Core.Interfaces;
using Ofgem.Lib.BUS.APIClient.Domain.Exceptions;
using Ofgem.API.BUS.BusinessAccounts.Client.Interfaces;

namespace Ofgem.API.BUS.Applications.Core;

/// <summary>
/// <see cref="IBusinessAccountsService"/> implementation
/// </summary>
public class BusinessAccountsService : IBusinessAccountsService
{
    private readonly IBusinessAccountsAPIClient _businessAccountsAPIClient;
    public BusinessAccountsService(IBusinessAccountsAPIClient businessAccountsAPIClient)
    {
        _businessAccountsAPIClient = businessAccountsAPIClient ?? throw new ArgumentNullException(nameof(businessAccountsAPIClient));
    }

    public async Task<string?> GetBusinessAccountNameByIdAsync(Guid id)
    {
        var businessAccount = await _businessAccountsAPIClient.BusinessAccountRequestsClient.GetBusinessAccountAsync(id);

        if (businessAccount != null)
        {
            return businessAccount.BusinessName ?? businessAccount.TradingName;
        }

        throw new ResourceNotFoundException($"No Business Account with ID {id} was found");
    }

    public async Task<string> GetBusinessAccountEmailByIdAsync(Guid id)
    {
        var businessAccountEmail = await _businessAccountsAPIClient.BusinessAccountRequestsClient.GetBusinessAccountEmailById(id);
        if (businessAccountEmail != null)
        {
            return businessAccountEmail;
        }

        throw new ResourceNotFoundException($"No Business Account with ID {id} was found");
    }

    public async Task<string> GetBusinessAccountEmailByInstallerIdAsync(Guid installerId)
    {
        var businessAccountEmail = await _businessAccountsAPIClient.BusinessAccountRequestsClient.GetInstallerEmailByInstallerId(installerId);
        if (string.IsNullOrEmpty(businessAccountEmail))
        {
            throw new ResourceNotFoundException($"No ExternalUserAccount with ID {installerId} was found.");
        }

        return businessAccountEmail;
    }
}