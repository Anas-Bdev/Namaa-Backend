using MediatR;
using Microsoft.AspNetCore.Http;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Commands.UpdateProfileImage;
public sealed record UpdateProfileImageCommand(string UserId,IFormFile FormFile):IRequest<Result<Updated>>;