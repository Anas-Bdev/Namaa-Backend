using Microsoft.AspNetCore.Mvc;
using Namaa.Domain.Common.Results;

namespace Namaa.Api.Extensions;
public static class ErrorExtensions
{
    public static IActionResult ToProblem(this ControllerBase controller,Error error)
    {
        var statusCode =error.Type switch
        {
            ErrorKind.Validation => StatusCodes.Status400BadRequest,
            ErrorKind.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorKind.Forbidden => StatusCodes.Status403Forbidden,
            ErrorKind.NotFound => StatusCodes.Status404NotFound,
            ErrorKind.Conflict => StatusCodes.Status409Conflict,
            ErrorKind.Failure => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        return controller.Problem(title:error.Description,statusCode:statusCode);
    }
}