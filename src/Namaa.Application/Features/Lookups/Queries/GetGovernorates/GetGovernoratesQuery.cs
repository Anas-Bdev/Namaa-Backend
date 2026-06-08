using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lookups.Queries.GetGovernorates;
public sealed record GetGovernoratesQuery:IRequest<Result<List<GovernorateDto>>>;