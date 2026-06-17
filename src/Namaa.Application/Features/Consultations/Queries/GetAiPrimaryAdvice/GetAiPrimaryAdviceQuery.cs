using MediatR;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;
namespace Namaa.Application.Features.Consultations.Queries.GetAiPrimaryAdvice;
public sealed record GetAiPrimaryAdviceQuery(
    string Title,
    string Description,
    string? ImageUrl
):IRequest<Result<AiPrimaryAdviceDto>>;