using EmailSender.AWS.Lambda.Dtos;
using System.Net;
using System.Net.Mail;

namespace EmailSender.AWS.Lambda.Utils;

public static class EmailUtils
{
    public static ResponseDto SendEmail(EmailDto dto, bool test = false)
    {
        try
        {
            var emailSender = Environment.GetEnvironmentVariable("EmailSender");
            var emailPassword = Environment.GetEnvironmentVariable("EmailPassword");
            var smtp = Environment.GetEnvironmentVariable("Smtp");
            var smtpPort = Environment.GetEnvironmentVariable("SmtpPort");

            if (emailSender == null || emailPassword == null || smtp == null || smtpPort == null)
            {
                return new ResponseDto("Error in getting credentials", 500);
            }

            MailMessage newMail = new();
            SmtpClient client = new(smtp);
            newMail.From = new MailAddress(emailSender, "NOT REPLY");
            newMail.To.Add(dto.Email);

            newMail.Subject = dto.Title ?? "Automatic Title";
            newMail.IsBodyHtml = true;
            newMail.Body = dto.Html ?? "<h1>Hello!</h1><h2>This is an email sent by AWS LAMBDA with .NET 6.0, thanks for testing it!!!</h2>";

            if (test)
            {
                client.EnableSsl = false;
                client.Host = "localhost";
            }
            else
            {
                client.EnableSsl = true;
            }
            client.Port = int.Parse(smtpPort);
            client.Credentials = new NetworkCredential(emailSender, emailPassword);
            client.Send(newMail);

            return new ResponseDto("Email send with success !!!", 200);

        }
        catch (Exception)
        {
            throw;
        }
    }
}
