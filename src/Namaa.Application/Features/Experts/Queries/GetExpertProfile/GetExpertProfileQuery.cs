using MediatR;
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Queries.GetExpertProfile;
public sealed record GetExpertProfileQuery(Guid UserId) : IRequest<Result<ExpertProfileDto>>;