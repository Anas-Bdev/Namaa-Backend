using MediatR;
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Queries.GetExpertProfileById;
public sealed record GetExpertProfileByIdQuery(Guid ExpertId):IRequest<Result<ExpertListItemDto>>;