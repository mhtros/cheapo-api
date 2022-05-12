namespace Cheapo.Api.Classes;

public interface IErrorResponse
{
    public IEnumerable<string?> Errors { get; set; }
}

public interface IDataResponse<T>
{
    public T? Data { get; set; }
}

public class ErrorResponse : IErrorResponse
{
    public ErrorResponse()
    {
    }

    public ErrorResponse(IEnumerable<string?> errors)
    {
        Errors = errors;
    }

    public IEnumerable<string?>? Errors { get; set; }
}

public class DataResponse<T> : IDataResponse<T>
{
    public DataResponse()
    {
    }

    public DataResponse(T? data)
    {
        Data = data;
    }

    public T? Data { get; set; }
}

public class Response<T> : IDataResponse<T>, IErrorResponse
{
    public Response()
    {
    }

    public Response(T? data, IEnumerable<string?> errors)
    {
        Data = data;
        Errors = errors;
    }

    public Response(T? data)
    {
        Data = data;
        Errors = null;
    }

    public T? Data { get; set; }
    public IEnumerable<string?>? Errors { get; set; }
}