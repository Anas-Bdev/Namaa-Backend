using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetAiPrimaryAdvice;

public class GetAiPrimaryAdviceQueryHandler(IAiConsultantService aiConsultantService) : IRequestHandler<GetAiPrimaryAdviceQuery, Result<AiPrimaryAdviceDto>>
{
    public async Task<Result<AiPrimaryAdviceDto>> Handle(GetAiPrimaryAdviceQuery request, CancellationToken cancellationToken)
    {
        var aiResponse = await aiConsultantService.GeneratePrimaryAdviceAsync(
            request.Title, 
            request.Description, 
            request.ImageUrl,
            cancellationToken);

        return aiResponse;
    }
}