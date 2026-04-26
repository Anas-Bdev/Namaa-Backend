using MediatR;
using Microsoft.AspNetCore.Http;
using Namaa.Application.Features.Account.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Commands.UpdateProfileImage;
public sealed record UpdateProfileImageCommand(string UserId,IFormFile FormFile):IRequest<Result<UploadImageDto>>;