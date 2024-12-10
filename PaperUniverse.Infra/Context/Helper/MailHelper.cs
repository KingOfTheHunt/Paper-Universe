using System.Net;
using System.Net.Mail;
using PaperUniverse.Core.Contexts;

namespace PaperUniverse.Infra.Context.Helper;

public static class MailHelper
{
    public static MailMessage CreateMailMessage(string from, string to, string subject, string body)
    {
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(from);
        mailMessage.To.Add(to);
        mailMessage.Subject = subject;
        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;
        
        return mailMessage;
    }

    public static async Task SendMailAsync(MailMessage mailMessage, CancellationToken cancellationToken)
    {
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = Configuration.Smtp.Host;
        smtpClient.Port = Configuration.Smtp.Port;
        smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.Login, 
            Configuration.Smtp.Password);
        smtpClient.EnableSsl = true;
        
        await smtpClient.SendMailAsync(mailMessage, cancellationToken);
    }
}