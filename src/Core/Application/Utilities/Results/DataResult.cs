namespace Application.Utilities.Results;
public class DataResult<T> : Result, IDataResult<T>
{
    public DataResult(T data, bool success, string message) : base(success, message)
    {
        Data = data;
    }

    public DataResult(T data, bool success) : base(success)
    {
        Data = data;
    }


    public DataResult(bool success) : base(success)
    {

    }

    public T Data { get; }

}