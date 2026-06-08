using MediatR;

namespace Namaa.Application.Common.Interfaces;
public interface ICachedQuery<TResponse>:IRequest<TResponse>
{
   string CacheKey {get;}
   string [] Tags {get;}
   TimeSpan Expiration {get;}
}