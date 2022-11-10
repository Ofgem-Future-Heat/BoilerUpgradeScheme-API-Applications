namespace Ofgem.API.BUS.Applications.Core.Interfaces;

/// <summary>
/// Interface for Business Accounts API functions
/// </summary>
public interface IBusinessAccountsService
{
    /// <summary>
    /// Gets the name of the business account
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public Task<string?> GetBusinessAccountNameByIdAsync (Guid id);

    /// <summary>
    /// Gets the email id of Installer by business account id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>email id of installer</returns>
    public Task<string> GetBusinessAccountEmailByIdAsync(Guid id);

    /// <summary>
    /// Gets Installer Email by Installer Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<string> GetBusinessAccountEmailByInstallerIdAsync(Guid installerId);
}
