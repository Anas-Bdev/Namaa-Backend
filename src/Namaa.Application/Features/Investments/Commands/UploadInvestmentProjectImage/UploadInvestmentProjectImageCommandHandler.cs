using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Account.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.UploadInvestmentProjectImage;

public class UploadInvestmentProjectImageCommandHandler(IFileService fileService) : IRequestHandler<UploadInvestmentProjectImageCommand, Result<UploadImageDto>>
{
    public async Task<Result<UploadImageDto>> Handle(UploadInvestmentProjectImageCommand request, CancellationToken cancellationToken)
    {
         var imageUrl = await fileService.UploadFileAsync(
            request.FormFile,
            "investment-projects/images");

        return new UploadImageDto
        {
            ImageUrl = imageUrl
        };
    }
}