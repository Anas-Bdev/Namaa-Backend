using MediatR;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investors.Queries.GetInvestorProfile;

public sealed record GetInvestorProfileQuery(Guid UserId):IRequest<Result<InvestorProfileDto>>;