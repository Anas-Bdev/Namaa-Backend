using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Marketplace;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Marketplace.Commands.CancelOrder;
using Namaa.Application.Features.Marketplace.Commands.ConfirmOrder;
using Namaa.Application.Features.Marketplace.Commands.CreateListing;
using Namaa.Application.Features.Marketplace.Commands.CreateOrder;
using Namaa.Application.Features.Marketplace.Commands.CreateRating;
using Namaa.Application.Features.Marketplace.Commands.DeleteListing;
using Namaa.Application.Features.Marketplace.Commands.PayOrder;
using Namaa.Application.Features.Marketplace.Commands.UpdateListing;
using Namaa.Application.Features.Marketplace.Queries.GetAllListings;
using Namaa.Application.Features.Marketplace.Queries.GetFarmerRatings;
using Namaa.Application.Features.Marketplace.Queries.GetListingById;
using Namaa.Application.Features.Marketplace.Queries.GetMyListings;
using Namaa.Application.Features.Marketplace.Queries.GetMyOrders;
using Namaa.Application.Features.Marketplace.Queries.GetMySales;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;

[Route("api/marketplace")]
[ApiController]
public class MarketplaceController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // ========== Listings ==========

    [HttpGet("listings")]
    public async Task<IActionResult> GetAllListings(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = new GetAllListingsQuery(pageNumber, pageSize);
        var result = await sender.Send(query, ct);
        return result.Match(listings => Ok(listings), errors => this.ToProblem(errors));
    }

    [HttpGet("listings/{id}")]
    public async Task<IActionResult> GetListingById(Guid id, CancellationToken ct)
    {
        var query = new GetListingByIdQuery(id);
        var result = await sender.Send(query, ct);
        return result.Match(listing => Ok(listing), errors => this.ToProblem(errors));
    }

    [HttpGet("listings/my-listings")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> GetMyListings(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = new GetMyListingsQuery(UserId, pageNumber, pageSize);
        var result = await sender.Send(query, ct);
        return result.Match(listings => Ok(listings), errors => this.ToProblem(errors));
    }

    [HttpPost("listings")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> CreateListing(
        [FromBody] CreateListingRequest request,
        CancellationToken ct)
    {
        var command = new CreateListingCommand(
            request.SeedingCycleId,
            UserId,
            request.Title,
            request.Description,
            request.Unit,
            request.PricePerUnit,
            request.DiscountPrice,
            request.QuantityAvailable,
            request.ImageUrl,
            request.HarvestDate
        );
        var result = await sender.Send(command, ct);
        return result.Match(listing => Ok(listing), errors => this.ToProblem(errors));
    }

    [HttpPut("listings/{id}")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> UpdateListing(
        Guid id,
        [FromBody] UpdateListingRequest request,
        CancellationToken ct)
    {
        var command = new UpdateListingCommand(
            id,
            UserId,
            request.Title,
            request.Description,
            request.Unit,
            request.PricePerUnit,
            request.DiscountPrice,
            request.QuantityAvailable,
            request.ImageUrl,
            request.HarvestDate
        );
        var result = await sender.Send(command, ct);
        return result.Match(listing => Ok(listing), errors => this.ToProblem(errors));
    }

    [HttpDelete("listings/{id}")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> DeleteListing(Guid id, CancellationToken ct)
    {
        var command = new DeleteListingCommand(id, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    // ========== Orders ==========

    [HttpPost("orders")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderRequest request,
        CancellationToken ct)
    {
        var command = new CreateOrderCommand(UserId, request.ListingId, request.Quantity);
        var result = await sender.Send(command, ct);
        return result.Match(order => Ok(order), errors => this.ToProblem(errors));
    }

    [HttpPut("orders/{id}/confirm")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> ConfirmOrder(Guid id, CancellationToken ct)
    {
        var command = new ConfirmOrderCommand(id, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(order => Ok(order), errors => this.ToProblem(errors));
    }

    [HttpPut("orders/{id}/pay")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> PayOrder(Guid id, CancellationToken ct)
    {
        var command = new PayOrderCommand(id, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(order => Ok(order), errors => this.ToProblem(errors));
    }

    [HttpPut("orders/{id}/cancel")]
    [Authorize]
    public async Task<IActionResult> CancelOrder(Guid id, CancellationToken ct)
    {
        var command = new CancelOrderCommand(id, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(order => Ok(order), errors => this.ToProblem(errors));
    }

    [HttpGet("orders/my-orders")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> GetMyOrders(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = new GetMyOrdersQuery(UserId, pageNumber, pageSize);
        var result = await sender.Send(query, ct);
        return result.Match(orders => Ok(orders), errors => this.ToProblem(errors));
    }

    [HttpGet("orders/my-sales")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> GetMySales(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = new GetMySalesQuery(UserId, pageNumber, pageSize);
        var result = await sender.Send(query, ct);
        return result.Match(sales => Ok(sales), errors => this.ToProblem(errors));
    }

    // ========== Ratings ==========

    [HttpPost("ratings")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> CreateRating(
        [FromBody] CreateRatingRequest request,
        CancellationToken ct)
    {
        var command = new CreateRatingCommand(
            request.OrderId,
            UserId,
            request.RatingValue,
            request.Comment
        );
        var result = await sender.Send(command, ct);
        return result.Match(rating => Ok(rating), errors => this.ToProblem(errors));
    }

    [HttpGet("ratings/farmer/{farmerId}")]
    public async Task<IActionResult> GetFarmerRatings(
        Guid farmerId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = new GetFarmerRatingsQuery(farmerId, pageNumber, pageSize);
        var result = await sender.Send(query, ct);
        return result.Match(ratings => Ok(ratings), errors => this.ToProblem(errors));
    }
}
