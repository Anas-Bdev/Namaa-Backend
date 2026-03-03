using System.Diagnostics.Contracts;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Common.Errors;
public static class User
{
    public static readonly Error NotFound=Error.NotFound("User.NotFound","User not found.");
    public static readonly Error InvalidCredentials=Error.Unauthorized("User.InvalidCredentials","Invalid credentials.");
    public static readonly Error EmailNotConfirmed=Error.Unauthorized("User.EmailNotConfirmed","Please confirm your email before logging in.");
    public static readonly Error Blocked=Error.Forbidden("User.Blocked","Your account is blocked.");
    public static readonly Error LockedOut=Error.Forbidden("User.LockedOut","Account is locked. Try again later.");
    public static readonly Error InvalidToken=Error.Validation("User.InvalidToken","Invalid confirmation token.");
    public static readonly Error DuplicateEmail=Error.Conflict("User.EmailExists","Email is already registered.");
    public static readonly Error DuplicateUserName=Error.Conflict("User.UserNameExists","Username is already taken.");
    public static readonly Error InvalidRole=Error.Validation("User.InvalidRole","The specified role is invalid or not allowed for registration.");
    public static Error CreateFailed(string msg) => Error.Validation("User.CreateFailed",msg);
    public static Error AddRoleFailed(string msg) => Error.Validation("User.AddRoleFailed",msg);
    public static Error EmailConfirmationFailed(string msg) => Error.Validation("User.EmailConfirmationFailed",msg);
    public static Error InvalidParameters=Error.Validation("User.InvalidConfirmEmailParameters","Invalid email confirmation parameters.");
    public static Error InvalidConfirmationLink=Error.Validation("User.InvalidConfirmationLink","Invalid Confirmation link please try again.");

}