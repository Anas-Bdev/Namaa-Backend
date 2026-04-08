using MediatR;
using Microsoft.AspNetCore.Http;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Commands.RegisterExpert;
public sealed record RegisterExpertCommand(
   string Email,
   string Password,
   string FirstName,
   string? LastName,
   string? PhoneNumber,
   IFormFile CvFile
):IRequest<Result<Created>>;