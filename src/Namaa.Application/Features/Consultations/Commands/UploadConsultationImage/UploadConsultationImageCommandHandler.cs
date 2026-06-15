using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Account.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.UploadConsultationImage;

public class UploadConsultationImageCommandHandler(IFileService fileService) : IRequestHandler<UploadConsultationImageCommand, Result<UploadImageDto>>
{
    public async Task<Result<UploadImageDto>> Handle(UploadConsultationImageCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = await fileService.UploadFileAsync(
            request.FormFile, 
            "consultations/images"
        );

        return new UploadImageDto
        {
            ImageUrl=imageUrl
        };
    }
}