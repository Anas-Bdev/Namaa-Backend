using MediatR;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Farmers.Queries.GetFarmerProfile;
public sealed record GetFarmerProfileQuery(Guid UserId):IRequest<Result<FarmerProfileDto>>;