using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Namaa.Application.Common.Interfaces;
using Namaa.Infrastructure.Settings;

namespace Namaa.Infrastructure.Services;

public class EmailSender(IOptions<SmtpOptions> options) : IEmailSender
{
    private readonly SmtpOptions _smtp = options.Value;
    private static readonly HttpClient _httpClient = new();

    public async Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default)
    {
        var emailRequest = new
        {
            sender = new { name = _smtp.SenderName, email = _smtp.SenderEmail },
            to = new[] { new { email = to } },
            subject = subject,
            htmlContent = body
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.brevo.com/v3/smtp/email");
        
        request.Headers.Add("api-key", _smtp.Password);
        request.Content = new StringContent(
            JsonSerializer.Serialize(emailRequest), 
            Encoding.UTF8, 
            "application/json");

        var response = await _httpClient.SendAsync(request, ct);

        if (!response.IsSuccessStatusCode)
        {
            var errorDetails = await response.Content.ReadAsStringAsync(ct);
            Console.WriteLine($"Brevo API Error: {response.StatusCode} - {errorDetails}");
        }
    }
}
    
