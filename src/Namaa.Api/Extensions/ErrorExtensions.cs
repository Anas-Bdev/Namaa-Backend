using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Namaa.Domain.Common.Results;

namespace Namaa.Api.Extensions;
public static class ErrorExtensions
{
    public static IActionResult ToProblem(this ControllerBase controller,List<Error> errors)
    {
        if(errors.Count==0) return controller.Problem();
      if(errors.All(e => e.Type == ErrorKind.Validation))
        {
            var modelStateDictionary=new ModelStateDictionary();
            foreach(var error in errors)
            {
                modelStateDictionary.AddModelError(error.Code,error.Description);
            }
            return controller.ValidationProblem(modelStateDictionary);
        }
        return controller.ToProblem(errors[0]);
    }
    private static IActionResult ToProblem(this ControllerBase controller,Error error)
    {
        var statusCode=error.Type switch
        {
            ErrorKind.Conflict => StatusCodes.Status409Conflict,
            ErrorKind.Validation => StatusCodes.Status400BadRequest,
            ErrorKind.NotFound => StatusCodes.Status404NotFound,
            ErrorKind.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorKind.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };
        return controller.Problem(statusCode:statusCode,title:error.Description);
    }
}