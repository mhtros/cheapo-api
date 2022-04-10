using Cheapo.Api.Classes;

namespace Cheapo.Api.Interfaces.Services;

/// <summary>
///     Responsible for sending an email.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    ///     Sends an email asynchronously.
    /// </summary>
    /// <param name="message"><see cref="EmailMessage" />.</param>
    /// <returns><see cref="Task" />.</returns>
    public Task SendEmailAsync(EmailMessage message);
}