using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetAiPrimaryAdvice;

public class GetAiPrimaryAdviceQueryHandler(IAiConsultantService aiConsultantService) : IRequestHandler<GetAiPrimaryAdviceQuery, Result<string>>
{
    public async Task<Result<string>> Handle(GetAiPrimaryAdviceQuery request, CancellationToken cancellationToken)
    {
        var aiResponse = await aiConsultantService.GeneratePrimaryAdviceAsync(
            request.Title, 
            request.Description, 
            request.ImageUrl,
            cancellationToken);

        return aiResponse;
    }
}