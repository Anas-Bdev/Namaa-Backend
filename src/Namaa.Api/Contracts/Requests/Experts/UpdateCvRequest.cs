using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Experts.Commands.UpdateCv;

namespace Namaa.Api.Contracts.Requests.Experts;
public class UpdateCvRequest
{
    
  [Required(ErrorMessage = "Please upload your CV.")]
   public IFormFile CvFile {get;init;}=default!;

   public UpdateExpertCvCommand ToCommand(Guid id)
    {
        return new(id,CvFile);
    }
}