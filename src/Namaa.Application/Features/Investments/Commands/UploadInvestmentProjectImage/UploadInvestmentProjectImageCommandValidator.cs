using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.UploadInvestmentProjectImage;
public class UploadInvestmentProjectImageCommandValidator : AbstractValidator<UploadInvestmentProjectImageCommand>
{
    public UploadInvestmentProjectImageCommandValidator()
    {
         RuleFor(x => x.FormFile)
            .NotNull().WithMessage("Image file is required.")
            .Must(file => file.Length > 0)
            .WithMessage("Image file cannot be empty.");   
    }
}