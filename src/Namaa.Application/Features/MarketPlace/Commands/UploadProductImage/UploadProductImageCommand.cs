using MediatR;
using Microsoft.AspNetCore.Http;
using Namaa.Application.Features.Account.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.UploadProductImage;
public sealed record UploadProductImageCommand(IFormFile FormFile):IRequest<Result<UploadImageDto>>;
