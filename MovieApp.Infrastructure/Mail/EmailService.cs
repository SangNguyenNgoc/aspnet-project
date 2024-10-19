using System.Net;
using System.Net.Mail;

namespace MovieApp.Infrastructure.Mail;

public class EmailService
{

    private readonly MailConfig _mailConfig;

    public EmailService(MailConfig mailConfig)
    {
        _mailConfig = mailConfig;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_mailConfig.Username),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);

        using var smtpClient = new SmtpClient(_mailConfig.Host, _mailConfig.Port);
        smtpClient.Credentials = new NetworkCredential(_mailConfig.Username, _mailConfig.Password);
        smtpClient.EnableSsl = true;
        await smtpClient.SendMailAsync(mailMessage);
    }
}