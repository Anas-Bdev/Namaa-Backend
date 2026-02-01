using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Identity;
public static class UserErrors
{
     public static readonly Error UsernameRequired =
            Error.Validation("User.UsernameRequired", "Username is required.");

        public static readonly Error EmailRequired =
            Error.Validation("User.EmailRequired", "Email is required.");

        public static readonly Error InvalidEmail =
            Error.Validation("User.InvalidEmail", "Email format is invalid.");

        public static readonly Error PasswordRequired =
            Error.Validation("User.PasswordRequired", "Password is required.");

        public static readonly Error RoleIdRequired =
            Error.Validation("User.RoleIdRequired", "Role is required.");
}