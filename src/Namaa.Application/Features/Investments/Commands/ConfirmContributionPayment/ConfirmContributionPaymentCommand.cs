using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.ConfirmContributionPayment;
public sealed record ConfirmContributionPaymentCommand(
    Guid ProjectId,
    Guid ContributionId
):IRequest<Result<Updated>>;