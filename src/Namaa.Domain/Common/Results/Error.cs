using System.Diagnostics.Contracts;

namespace Namaa.Domain.Common.Results;
public sealed class Error
{
    public ErrorKind Type {get;}
    public String Code {get;}
    public String Description {get;}
    private Error(String code,String description,ErrorKind type)
    {
        this.Code=code;
        this.Description=description;
        this.Type=type;
    }
    public static Error Failure(String code=nameof(Failure),String description="General failure.") => new(code,description,ErrorKind.Failure);
    public static Error Unexpected(String code = nameof(Unexpected), String description = "Unexpected error.") => new(code, description, ErrorKind.Unexpected);
    public static Error Validation(String code = nameof(Validation), String description = "Validation error.") => new(code, description, ErrorKind.Validation);
    public static Error Conflict(String code = nameof(Conflict), String description = "Conflict error.") => new(code, description, ErrorKind.Conflict);
    public static Error NotFound(String code = nameof(NotFound), String description = "Not found error.") => new(code, description, ErrorKind.NotFound);
    public static Error Unauthorized(String code = nameof(Unauthorized), String description = "Unauthorized error.") => new(code, description, ErrorKind.Unauthorized);
    public static Error Forbidden(String code = nameof(Forbidden), String description = "Forbidden error.") => new(code, description, ErrorKind.Forbidden);
    public static Error Create(String code,String description,ErrorKind type) => new(code,description,type);
}