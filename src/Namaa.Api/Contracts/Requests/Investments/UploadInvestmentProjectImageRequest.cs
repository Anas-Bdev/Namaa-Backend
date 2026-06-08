using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.Investments;
public class UploadInvestmentProjectImageRequest
{
    [Required(ErrorMessage = "Project image is required.")]
    public IFormFile FormFile {get;init;}=default!;
}