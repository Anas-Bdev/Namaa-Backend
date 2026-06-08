namespace Namaa.Domain.Common.Results.Abstractions;
public interface IResult
{
    public List<Error> Errors {get;}
    public bool IsSuccess {get;}


}
public interface IResult<out TValue> : IResult
{
    public TValue Value {get;}
}