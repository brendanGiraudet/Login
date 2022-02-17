using Login.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Login.Services;

public class EmailSender : IEmailSender
{
    private readonly SendGridSettings _sendGridSettings;

    public EmailSender(IOptions<SendGridSettings> sendGridSettingsOptions)
    {
        _sendGridSettings = sendGridSettingsOptions.Value;
    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        return Execute(_sendGridSettings.Key!, subject, message, email);
    }

    public Task Execute(string apiKey, string subject, string message, string email)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage
        {
            From = new EmailAddress(_sendGridSettings.FromEmail, _sendGridSettings.FromName),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(email));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);

        return client.SendEmailAsync(msg);
    }
}