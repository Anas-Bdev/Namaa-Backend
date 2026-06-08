using System.Net;
using Namaa.Application.Common.Interfaces;

namespace Namaa.Infrastructure.Services;
public sealed class EmailTemplateService : IEmailTemplateService
{
  private readonly string _templateDirectory=Path.Combine(AppContext.BaseDirectory,"EmailTemplates");
    public string BuildConfirmEmailBody(string displayName, string confirmLink)
    {
     var safeName=WebUtility.HtmlEncode(displayName);
     var templatePath=Path.Combine(_templateDirectory,"ConfirmEmail.html");
     
     return File.ReadAllText(templatePath)
     .Replace("{{SafeName}}",safeName)
     .Replace("{{ConfirmLink}}",confirmLink);
    }

    public string BuildForgotPasswordBody(string userName, string resetCode)
    {
     var safeName=WebUtility.HtmlEncode(userName);
     var templatePath=Path.Combine(_templateDirectory,"ForgotPassword.html");

     return File.ReadAllText(templatePath)
     .Replace("{{SafeName}}",safeName)
     .Replace("{{ResetCode}}",resetCode);
    }
       
}