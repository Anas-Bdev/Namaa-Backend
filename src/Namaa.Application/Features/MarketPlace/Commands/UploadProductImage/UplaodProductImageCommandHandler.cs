using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Account.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.UploadProductImage;

public class UploadProductImageCommandHandler(IFileService fileService) : IRequestHandler<UploadProductImageCommand, Result<UploadImageDto>>
{
    public async Task<Result<UploadImageDto>> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
    {
         var imageUrl = await fileService.UploadFileAsync(
            request.FormFile,
            "farmers/products");

        return new UploadImageDto
        {
            ImageUrl = imageUrl
        };
    }
}