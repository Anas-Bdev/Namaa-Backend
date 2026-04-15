using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.Register;
public sealed record RegisterCommand(string Email,string Password,string Role,string FirstName,string? LastName,string? PhoneNumber):IRequest<Result<Success>>;