using MediatR;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investors.Queries.GetInvestorProfileById;
public sealed record GetInvestorProfileByIdQuery(Guid InvestorId):IRequest<Result<InvestorListItemDto>>;