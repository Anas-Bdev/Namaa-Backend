using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Infrastructure.Settings;


namespace Namaa.Infrastructure.Services;
public  class EmailSender(IOptions<SmtpOptions> options) :IEmailSender
{
    private readonly SmtpOptions _smtp=options.Value;
    public async Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default)
{
    var message = new MimeMessage();
    
    
    message.From.Add(new MailboxAddress("Namaa Support Team", _smtp.SenderEmail));
    message.To.Add(MailboxAddress.Parse(to));
    message.Subject = subject;
    message.Body = new BodyBuilder { HtmlBody = body }.ToMessageBody();

    using var client = new MailKit.Net.Smtp.SmtpClient();
    
    
    await client.ConnectAsync(_smtp.SmtpServer, _smtp.Port, SecureSocketOptions.StartTls, ct);
    
    
    await client.AuthenticateAsync(_smtp.Username, _smtp.Password, ct);
    
    await client.SendAsync(message, ct);
    await client.DisconnectAsync(true, ct);
}
}


    
