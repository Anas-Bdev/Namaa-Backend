using Namaa.Domain.Common.Results;

namespace Namaa.Application.Common.Interfaces;
public interface IEmailSender
{
    Task SendEmailAsync(string to,string subject,string body,CancellationToken ct=default);
}