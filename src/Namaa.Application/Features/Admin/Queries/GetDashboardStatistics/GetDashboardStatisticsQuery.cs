using MediatR;
using Namaa.Application.Features.Admin.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Admin.Queries.GetDashboardStatistics;
public sealed record GetDashboardStatisticsQuery():IRequest<Result<List<UserStatusCountDto>>>;