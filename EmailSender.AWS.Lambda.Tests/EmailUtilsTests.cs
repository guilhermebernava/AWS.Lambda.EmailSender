using EmailSender.AWS.Lambda.Dtos;
using EmailSender.AWS.Lambda.Utils;
using netDumbster.smtp;
using Xunit;

public class EmailUtilsTests
{
    [Fact]
    public void SendEmail_ValidDto_EmailSentSuccessfully()
    {
        var port = "9009";
        Environment.SetEnvironmentVariable("EmailSender", "test@example.com");
        Environment.SetEnvironmentVariable("EmailPassword", "password");
        Environment.SetEnvironmentVariable("Smtp", "smtp.example.com");
        Environment.SetEnvironmentVariable("SmtpPort", port);

        using (var smtpServer = SimpleSmtpServer.Start(int.Parse(port)))
        {
            var emailDto = new EmailDto("recipient@example.com");
            var response = EmailUtils.SendEmail(emailDto,true);

            Assert.Equal(200, response.statusCode);
            Assert.Equal("Email send with success !!!", response.Message);
        }
    }

    [Fact]
    public void SendEmail_InvalidCredentials_ErrorResponse()
    {
        Environment.SetEnvironmentVariable("EmailSender", null);
        Environment.SetEnvironmentVariable("EmailPassword", null);
        Environment.SetEnvironmentVariable("Smtp", null);

        var emailDto = new EmailDto("recipient@example.com");
        var response = EmailUtils.SendEmail(emailDto);


        Assert.Equal(500, response.statusCode);
        Assert.Equal("Error in getting credentials", response.Message);
    }
}
