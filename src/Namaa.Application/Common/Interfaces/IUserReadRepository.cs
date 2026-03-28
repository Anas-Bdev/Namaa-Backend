using Namaa.Application.Common.Models;
using Namaa.Application.Features.Identity.Dtos;

namespace Namaa.Application.Common.Interfaces;
public interface IUserReadRepository
{
    IQueryable<UserLookupModel> Query();
}