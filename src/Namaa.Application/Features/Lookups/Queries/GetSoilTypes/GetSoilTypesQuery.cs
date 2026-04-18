using MediatR;
using Namaa.Application.Features.Lookups.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.ReferenceData;

namespace Namaa.Application.Features.Lookups.Queries.GetSoilTypes;
public sealed record GetSoilTypesQuery:IRequest<Result<List<SoilTypeDto>>>;