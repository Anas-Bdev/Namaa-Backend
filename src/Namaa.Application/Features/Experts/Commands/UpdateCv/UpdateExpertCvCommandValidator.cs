using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Namaa.Application.Features.Experts.Commands.UpdateCv;
public class UpdateExpertCvCommandValidator : AbstractValidator<UpdateExpertCvCommand>
{
    public UpdateExpertCvCommandValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("A CV file is required to create a profile.");

        RuleFor(x => x.File)
            .Must(file => file == null || IsPdf(file))
            .WithMessage("Only PDF files are allowed for CV uploads.");

        RuleFor(x => x.File)
            .Must(file => file == null || file.Length <= 5 * 1024 * 1024)
            .WithMessage("CV file size must not exceed 5MB.");
            
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }

    private bool IsPdf(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLower();
        return extension == ".pdf" || file.ContentType == "application/pdf";
    }
}