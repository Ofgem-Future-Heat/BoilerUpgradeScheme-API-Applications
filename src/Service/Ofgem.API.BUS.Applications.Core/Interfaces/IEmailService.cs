using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Concrete;

namespace Ofgem.API.BUS.Applications.Core.Interfaces;

/// <summary>
/// Method definitions for transactional emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends a confirmation email to an installer after an application has been created.
    /// </summary>
    /// <param name="application">The created application.</param>
    /// <returns>A <see cref="SendEmailResult"/> object containing the status of the email.</returns>
    Task<SendEmailResult> SendInstallerPostApplicationEmailAsync(Application application);
}
