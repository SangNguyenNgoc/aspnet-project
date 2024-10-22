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
    if (string.IsNullOrEmpty(toEmail)) throw new ArgumentException("Email không được để trống.", nameof(toEmail));
    if (string.IsNullOrEmpty(subject)) throw new ArgumentException("Chủ đề không được để trống.", nameof(subject));
    if (string.IsNullOrEmpty(body)) throw new ArgumentException("Nội dung không được để trống.", nameof(body));

    var mailMessage = new MailMessage
    {
        From = new MailAddress("duckg2083@gmail.com"),
        Subject = subject,
        Body = body,
        IsBodyHtml = true,
    };

    mailMessage.To.Add(toEmail);

    using var smtpClient = new SmtpClient(_mailConfig.Host ?? throw new InvalidOperationException("Host không được để trống."), _mailConfig.Port);
    smtpClient.Credentials = new NetworkCredential(_mailConfig.Username, _mailConfig.Password);
    smtpClient.EnableSsl = true;
    await smtpClient.SendMailAsync(mailMessage);
}
}