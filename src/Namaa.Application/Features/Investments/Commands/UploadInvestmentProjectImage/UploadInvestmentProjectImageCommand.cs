using MediatR;
using Microsoft.AspNetCore.Http;
using Namaa.Application.Features.Account.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.UploadInvestmentProjectImage;
public sealed record UploadInvestmentProjectImageCommand(IFormFile FormFile):IRequest<Result<UploadImageDto>>;