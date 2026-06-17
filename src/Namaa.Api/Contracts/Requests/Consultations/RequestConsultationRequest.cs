namespace Namaa.Api.Contracts.Requests.Consultations;
public class RequestConsultationRequest
{
    public string Title {get;set;}=default!;

    public string Description {get;set;}=default!;
    
    public string? ImageUrl {get;set;}
}