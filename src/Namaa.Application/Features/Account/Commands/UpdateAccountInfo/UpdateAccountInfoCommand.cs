using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Commands.UpdateAccountInfo;
public sealed record UpdateAccountInfoCommand(string UserId,string FirstName,string? LastName,string? PhoneNumber):IRequest<Result<Updated>>;