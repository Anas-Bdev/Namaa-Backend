namespace Namaa.Application.Features.Experts.Dtos;
public class PendingExpertDto
{
    public Guid UserId {get;set;}
    public string FullName {get;set;}=string.Empty;
    public string Email {get;set;}=string.Empty;
    public string? PhoneNumber {get;set;}
    public string CvUrl {get;set;}=string.Empty;
}