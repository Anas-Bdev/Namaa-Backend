namespace Namaa.Application.Common.Interfaces;
public interface IEmailTemplateService
{
    string BuildConfirmEmailBody(string displayName,string confirmLink);
    string BuildForgotPasswordBody(string userName,string resetCode);
}