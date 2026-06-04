using System.Security.Claims;
using DotNetEnv;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.FarmerRatings;
using Namaa.Api.Contracts.Requests.ProductListings;
using Namaa.Api.Contracts.Requests.ProductOrders;
using Namaa.Api.Extensions;
using Namaa.Application.Features.MarketPlace.Commands.ArchiveListing;
using Namaa.Application.Features.MarketPlace.Commands.CancelOrder;
using Namaa.Application.Features.MarketPlace.Commands.ConfirmOrder;
using Namaa.Application.Features.MarketPlace.Commands.CreateListing;
using Namaa.Application.Features.MarketPlace.Commands.CreateOrder;
using Namaa.Application.Features.MarketPlace.Commands.CreateRating;
using Namaa.Application.Features.MarketPlace.Commands.DeliverOrder;
using Namaa.Application.Features.MarketPlace.Commands.PauseListing;
using Namaa.Application.Features.MarketPlace.Commands.PayOrder;
using Namaa.Application.Features.MarketPlace.Commands.ResumeListing;
using Namaa.Application.Features.MarketPlace.Commands.ShipOrder;
using Namaa.Application.Features.MarketPlace.Commands.UpdateListing;
using Namaa.Application.Features.MarketPlace.Commands.UploadProductImage;
using Namaa.Application.Features.MarketPlace.Queries.GetAllListings;
using Namaa.Application.Features.MarketPlace.Queries.GetFarmerListings;
using Namaa.Application.Features.MarketPlace.Queries.GetFarmerRatings;
using Namaa.Application.Features.MarketPlace.Queries.GetFarmerSales;
using Namaa.Application.Features.MarketPlace.Queries.GetListingById;
using Namaa.Application.Features.MarketPlace.Queries.GetOrderById;
using Namaa.Application.Features.MarketPlace.Queries.GetPendingOrders;
using Namaa.Application.Features.MarketPlace.Queries.GetTraderOrders;
using Namaa.Domain.Common.Constants;
using Org.BouncyCastle.Ocsp;
namespace Namaa.Api;
[Route("api/marketplace")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)] 
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public class MarketPlaceController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("listings")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> CreateListing([FromBody] CreateProductListingRequest request, CancellationToken ct)
    {
        var command = new CreateProductListingCommand(
            UserId, request.SeedingCycleId, request.CropId, request.Title, request.Description, 
            request.Unit, request.PricePerUnit, request.DiscountPrice, request.QuantityAvailable, 
            request.ImageUrl, request.HarvestDate
        );
        var result = await sender.Send(command, ct);
        return result.Match(response => CreatedAtAction(nameof(GetListing), new { listingId = response.Id }, response), errors => this.ToProblem(errors));
    }

    [HttpPut("listings/{listingId:guid}")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> UpdateListing(Guid listingId, [FromBody] UpdateProductListingRequest request, CancellationToken ct)
    {
        var command = new UpdateProductListingCommand(
            listingId, UserId, request.Title, request.Description, request.CropId, request.Unit, 
            request.PricePerUnit, request.DiscountPrice, request.QuantityAvailable, request.ImageUrl, request.HarvestDate
        );
        var result = await sender.Send(command, ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPost("listings/upload-image")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile formFile, CancellationToken ct)
    {
        var command = new UploadProductImageCommand(formFile);
        var result = await sender.Send(command, ct);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpPut("listings/{listingId:guid}/archive")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> ArchiveListing(Guid listingId, CancellationToken ct)
    {
        var command = new ArchiveProductListingCommand(listingId, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("listings/{listingId:guid}/pause")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> PauseListing(Guid listingId, CancellationToken ct)
    {
        var command = new PauseProductListingCommand(listingId, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("listings/{listingId:guid}/resume")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> ResumeListing(Guid listingId, CancellationToken ct)
    {
        var command = new ResumeProductListingCommand(listingId, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPost("orders")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateProductOrderRequest request, CancellationToken ct)
    {
        var command = new CreateProductOrderCommand(
            UserId, request.ProductListingId, request.Quantity, request.Governorate, request.CityOrVillage, 
            request.NeighborhoodOrStreet, request.LandMark, request.DeliveryNotes
        );
        var result = await sender.Send(command, ct);
        return result.Match(id => CreatedAtAction(nameof(GetOrderById), new { orderId = id }, id), errors => this.ToProblem(errors));
    }

    [HttpGet("orders/{orderId:guid}")]
    [Authorize(Roles = AppRoles.Trader + "," + AppRoles.Farmer)]
    public async Task<IActionResult> GetOrderById(Guid orderId, CancellationToken ct)
    {
        var result = await sender.Send(new GetProductOrderByIdQuery(orderId), ct);
        return result.Match(order => Ok(order), errors => this.ToProblem(errors));
    }

    [HttpPut("orders/{orderId:guid}/confirm")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> ConfirmOrder(Guid orderId, CancellationToken ct)
    {
        var result = await sender.Send(new ConfirmProductOrderCommand(orderId), ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("orders/{orderId:guid}/pay")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> PayOrder(Guid orderId, CancellationToken ct)
    {
        var result = await sender.Send(new PayProductOrderCommand(orderId, UserId), ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("orders/{orderId:guid}/ship")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> ShipOrder(Guid orderId, [FromBody] ShipProductOrderRequest request, CancellationToken ct)
    {
        var command=new ShipProductOrderCommand(
          orderId,
          request.EstimatedArrivalDate  
        );
        
        var result=await sender.Send(command,ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("orders/{orderId:guid}/deliver")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> DeliverOrder(Guid orderId, CancellationToken ct)
    {
        var result = await sender.Send(new DeliverProductOrderCommand(orderId, UserId), ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("orders/{orderId:guid}/cancel")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> CancelOrder(Guid orderId, CancellationToken ct)
    {
        var command = new CancelProductOrderCommand(orderId, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPost("orders/{orderId:guid}/rating")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> CreateRating(Guid orderId, [FromBody] CreateFarmerRatingRequest request, CancellationToken ct)
    {
        var command = new CreateFarmerRatingCommand(orderId, request.RatingValue, request.Comment, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(_ => CreatedAtAction(nameof(GetOrderById), new { orderId }, new { message = "Rating created successfully" }), errors => this.ToProblem(errors));
    }

    [HttpGet("listings/{listingId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetListing(Guid listingId, CancellationToken ct)
    {
        var result = await sender.Send(new GetProductListingByIdQuery(listingId), ct);
        return result.Match(listing => Ok(listing), errors => this.ToProblem(errors));
    }

    [HttpGet("listings")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllListings([FromQuery] GetAllListingsRequest request, CancellationToken ct = default)
    {
        var query = new GetAllListingsQuery(request.Category, request.Location, request.MinPrice, request.MaxPrice, request.PageNumber, request.PageSize);
        var result = await sender.Send(query, ct);
        return result.Match(list => Ok(list), errors => this.ToProblem(errors));
    }
    
    [HttpGet("farmers/my-listings")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> GetMyListings(CancellationToken ct)
    {
        var result = await sender.Send(new GetFarmerProductListingsQuery(UserId), ct);
        return result.Match(list => Ok(list), errors => this.ToProblem(errors));
    }

    [HttpGet("farmers/my-sales")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> GetMySales(CancellationToken ct)
    {
        var result = await sender.Send(new GetFarmerProductOrderSalesQuery(UserId), ct);
        return result.Match(list => Ok(list), errors => this.ToProblem(errors));
    }

    [HttpGet("farmers/{farmerId:guid}/ratings")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFarmerRatings(Guid farmerId, CancellationToken ct)
    {
        var result = await sender.Send(new GetFarmerRatingsByIdQuery(farmerId), ct);
        return result.Match(ratings => Ok(ratings), errors => this.ToProblem(errors));
    }

    [HttpGet("orders/my-orders")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> GetMyOrders(CancellationToken ct)
    {
        var result = await sender.Send(new GetTraderProductOrdersQuery(UserId), ct);
        return result.Match(orders => Ok(orders), errors => this.ToProblem(errors));
    }

    [HttpGet("orders/pending")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> GetPendingOrders(CancellationToken ct)
    {
        var result = await sender.Send(new GetPendingProductOrdersQuery(UserId), ct);
        return result.Match(orders => Ok(orders), errors => this.ToProblem(errors));
    }
}