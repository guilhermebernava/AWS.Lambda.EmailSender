using Amazon.Lambda.Core;
using EmailSender.AWS.Lambda.Dtos;
using EmailSender.AWS.Lambda.Utils;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace EmailSender.AWS.Lambda;

public class Function
{
    /// <summary>
    /// An function who sends emails with SMTP
    /// </summary>
    /// <param name="emailDto">JSON the needs to send email</param>
    /// <param name="context"></param>
    /// <returns>Result of function</returns>
    public ResponseDto Handler(EmailDto emailDto, ILambdaContext context)
    {
        return EmailUtils.SendEmail(emailDto);
    }
}
