using MediatR;
using Microsoft.AspNetCore.Http;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Commands.UpdateCv;
public sealed record UpdateExpertCvCommand(
    Guid UserId,
    IFormFile File
) : IRequest<Result<Updated>>;