using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using Namaa.Application.Common.Interfaces;

namespace Namaa.Application.Common.Behaviors;

public class PerformanceBehavior<TRequest, TResponse>(
    ILogger<TRequest> logger,
    IUser user) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly Stopwatch _timer=new();
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

    _timer.Start();
    
    var response=await next();

    _timer.Stop();

    var elapsedMilliseconds = _timer.ElapsedMilliseconds;

    if(elapsedMilliseconds > 500)
        {
            var requestName=typeof(TRequest).Name;
            var userId=user.Id;

            logger.LogWarning(
                "Namaa Long Running Request: {Name} ({ElapsedMilliseconds} ms) by User: {UserId} {@Request}", 
                requestName, elapsedMilliseconds, userId, request);
        }

        return response;
        
        }

    }
