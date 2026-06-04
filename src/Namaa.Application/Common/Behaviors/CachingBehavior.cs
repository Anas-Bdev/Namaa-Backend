using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results.Abstractions;

namespace Namaa.Application.Common.Behaviors;

public class CachingBehavior<TRequest, TResponse>(
    HybridCache cache,
    ILogger<CachingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Checking cache for {CacheKey}", request.CacheKey);

        var cachedResult= await cache.GetOrCreateAsync<TResponse>(
            request.CacheKey,
            _ => new ValueTask<TResponse>(default(TResponse)!),
            new HybridCacheEntryOptions {Flags= HybridCacheEntryFlags.DisableUnderlyingData},
            cancellationToken:cancellationToken);
            
        if(cachedResult is not null)
        {
            logger.LogInformation("Cache hit for {CacheKey}", request.CacheKey);
            return cachedResult;
        }

        logger.LogInformation("Cache miss for {CacheKey}. Executing query...",request.CacheKey);
        var response=await next();

        if(response is IResult res && res.IsSuccess)
        {
            logger.LogInformation("Caching successful result for {CacheKey}", request.CacheKey);
            
            await cache.SetAsync(
                request.CacheKey,
                response,
                options: new HybridCacheEntryOptions { Expiration = request.Expiration },
                tags: request.Tags,
                cancellationToken: cancellationToken);
        }

        return response;
        }
        
    }
