using System.Security;
using System.Text.Json.Serialization;
using Namaa.Domain.Common.Results.Abstractions;

namespace Namaa.Domain.Common.Results;
public static class Result
{
    public static Success Success => default;
    public static Created Created => default;
    public static Deleted Deleted => default;
    public static Updated Updated => default;
}
public class Result<TValue> : IResult<TValue>
{
    private readonly TValue? _value;
    private readonly List<Error>? _errors;
     public bool IsSuccess {get;}

[JsonConstructor]
[Obsolete("This constructor is for the JSON serializer only. Use implicit operators or Match instead.", true)]
public Result(TValue? value, List<Error>? errors, bool isSuccess)
{
    _value = value;
    _errors = errors;
    IsSuccess = isSuccess;
}

    private Result(TValue value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        IsSuccess=true;
        _value=value;
    }
    private Result(Error error)
    {
        _errors=[error];
        IsSuccess=false;
    }
    private Result(List<Error> errors)
    {
        if(errors is null || errors.Count==0)
        throw new ArgumentException("Cannot create an ErrorOr<TValue> from an empty collection of errors. Provide at least one error.", nameof(errors));
        _errors=errors;
        IsSuccess=false;
    }
    public TValue Value => IsSuccess ? _value! : default!; 
    public List<Error> Errors => IsError ? _errors! : [];
    public Error TopError => (_errors?.Count>0) ? _errors[0]! : default!;
    public bool IsError => !IsSuccess;
    public static implicit operator Result<TValue>(TValue value ) => new(value);
    public static implicit operator Result<TValue>(Error error) => new(error);
    public static implicit operator Result<TValue>(List<Error> errors) => new(errors);
    public TNextValue Match <TNextValue>(Func <TValue,TNextValue> onValue,Func <List<Error>,TNextValue> onError)
    {
        return IsSuccess ? onValue(Value) : onError(Errors);
    }
}
public readonly record struct Success;
public readonly record struct Updated;
public readonly record struct Deleted;
public readonly record struct Created;
