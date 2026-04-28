using MediatR;
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Queries.GetPendingExperts;
public sealed record GetPendingExpertsQuery():IRequest<Result<List<PendingExpertDto>>>;