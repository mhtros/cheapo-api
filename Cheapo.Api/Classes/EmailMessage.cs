using MimeKit;

namespace Cheapo.Api.Classes;

/// <summary>
///     Represents the struct of a simple email.
/// </summary>
public class EmailMessage
{
    public EmailMessage(IEnumerable<string?> to, string subject, string content)
    {
        To = new List<MailboxAddress>();
        To.AddRange(to.Select(address => new MailboxAddress(address, address)));
        Subject = subject;
        Content = content;
    }

    /// <summary>
    ///     Email receivers.
    /// </summary>
    public List<MailboxAddress> To { get; set; }

    /// <summary>
    ///     Email subject.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    ///     Email content.
    /// </summary>
    public string Content { get; set; }
}