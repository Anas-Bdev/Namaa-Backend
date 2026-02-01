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
    private readonly Error? _error;
    private Result(TValue value)
    {
        IsSuccess=true;
        _value=value;
    }
    private Result(Error error)
    {
        IsSuccess=false;
        _error=error;
    }
    public TValue Value => _value!; 
    public Error Error => _error!;
    public bool IsSuccess {get;}
    public bool IsError => !IsSuccess;
    public static implicit operator Result<TValue>(TValue value ) => new(value);
    public static implicit operator Result<TValue>(Error error) => new(error);
    public TNextValue Match <TNextValue>(Func <TValue,TNextValue> onValue,Func <Error,TNextValue> onError)
    {
        return IsSuccess ? onValue(Value) : onError(Error);
    }
}
public readonly record struct Success;
public readonly record struct Updated;
public readonly record struct Deleted;
public readonly record struct Created;
