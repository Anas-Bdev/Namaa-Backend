using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Namaa.Api.Contracts.Requests.Account;
public class UpdateAccountInfoRequest
{
    [Required(ErrorMessage ="First name is required.")]
    public string FirstName {get;init;}=default!;
    public string? LastName {get;init;}
    [Phone]
    public string? PhoneNumber {get;init;}
}