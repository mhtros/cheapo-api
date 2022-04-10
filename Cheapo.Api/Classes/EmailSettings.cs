namespace Cheapo.Api.Classes;

/// <summary>
///     Represents the information needed to send an email.
/// </summary>
public class EmailSettings
{
    /// <summary>
    ///     Sender email.
    /// </summary>
    public string? From { get; set; }

    /// <summary>
    ///     The server that will forward the emails.
    /// </summary>
    public string? SmtpServer { get; set; }

    /// <summary>
    ///     SMPT port number that will used to forward the emails.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    ///     Sender email (used for authentication purposes).
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    ///     The email password (used for authentication purposes).
    /// </summary>
    public string? Password { get; set; }
}