namespace Namaa.Infrastructure.Settings;
public sealed class SmtpOptions
{
    public string SmtpServer {get;set;}=string.Empty;
    public int Port {get;set;}
    public string Username {get;set;}=string.Empty;
    public string Password {get;set;}=string.Empty;
    public string SenderEmail {get;init;}=string.Empty;
    public string SenderName {get;set;}=string.Empty;
}