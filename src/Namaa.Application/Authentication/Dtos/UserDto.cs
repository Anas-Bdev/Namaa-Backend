namespace Namaa.Application.Authentication.Dtos;
public sealed record UserDto(Guid Id,String UserName,String Email,IEnumerable<String> roles);