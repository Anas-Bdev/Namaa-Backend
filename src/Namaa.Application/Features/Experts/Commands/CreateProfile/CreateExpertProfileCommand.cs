using MediatR;
using Microsoft.AspNetCore.Http;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Commands.CreateProfile;
public sealed record CreateExpertProfileCommand(Guid UserId,IFormFile File):IRequest<Result<Created>>;