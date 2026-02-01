namespace Namaa.Domain.Common.Results.Abstractions;
public interface IResult
{
    public Error Error {get;}
    public bool IsSuccess {get;}


}
public interface IResult<out TValue> : IResult
{
    public TValue Value {get;}
}