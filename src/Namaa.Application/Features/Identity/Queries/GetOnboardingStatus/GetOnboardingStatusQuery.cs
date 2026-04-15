using MediatR;
using Namaa.Application.Features.Identity.Dtos;

namespace Namaa.Application.Features.Identity.Queries.GetOnboardingStatus;
public sealed record GetOnboardingStatusQuery(Guid UserId,string Role):IRequest<OnboardingStatus>;