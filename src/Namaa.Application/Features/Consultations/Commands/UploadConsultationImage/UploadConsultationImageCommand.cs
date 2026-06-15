using MediatR;
using Microsoft.AspNetCore.Http;
using Namaa.Application.Features.Account.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.UploadConsultationImage;
public sealed record UploadConsultationImageCommand(
    IFormFile FormFile
):IRequest<Result<UploadImageDto>>;