using System.Net;

namespace DemoTienda.Application.DTOs;

public record Result<T> : Result
{
    public T? Value { get; }

    protected Result(bool isSuccess, T? value, Error? error, string? title, HttpStatusCode statusCode) : base(isSuccess, error, title, statusCode)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, value, null, null, HttpStatusCode.OK);
    public static Result<T> Success(T value, HttpStatusCode statusCode) => new(true, value, null, null, statusCode);
    public static new Result<T> Failure(string paramName, string message)
        => new(false, default, new Error(paramName, message), "One or more validation errors occurred.", HttpStatusCode.BadRequest);
    public static new Result<T> Failure(string paramName, string message, HttpStatusCode statusCode)
        => new(false, default, new Error(paramName, message), "One or more validation errors occurred.", statusCode);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error.ParamName, error.Message);
}

public record Result
{
    public bool IsSuccess { get; }
    public string? Title { get; }
    public Error? Error { get; }
    public HttpStatusCode StatusCode { get; }

    protected Result(bool isSuccess, Error? error, string? title, HttpStatusCode statusCode)
    {
        IsSuccess = isSuccess;
        Error = error;
        Title = title;
        StatusCode = statusCode;
    }

    public static Result Success() => new(true, null, null, HttpStatusCode.OK);
    public static Result Success(HttpStatusCode statusCode) => new(true, null, null, statusCode);
    public static Result Failure(string paramName, string message)
        => new(false, new Error(paramName, message), "One or more validation errors occurred.", HttpStatusCode.BadRequest);
    public static Result Failure(string paramName, string message, HttpStatusCode statusCode)
        => new(false, new Error(paramName, message), "One or more validation errors occurred.", statusCode);
}

public class Error
{
    public string ParamName { get; }
    public string Message { get; }

    public Error(string paramName, string message)
    {
        ParamName = paramName;
        Message = message;
    }

    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string> { { ParamName, Message } };
    }
}