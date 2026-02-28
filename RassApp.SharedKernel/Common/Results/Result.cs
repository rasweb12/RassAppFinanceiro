using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.SharedKernel.Common.Results;

public class Result
{
    public bool Success { get; }
    public bool Failure => !Success;
    public Error Error { get; }

    protected Result(bool success, Error error)
    {
        Success = success;
        Error = error;
    }

    public static Result Ok() =>
        new(true, Error.None);

    public static Result Fail(Error error) =>
        new(false, error);
}

public class Result<T> : Result
{
    public T? Value { get; }

    protected Result(bool success, T? value, Error error)
        : base(success, error)
    {
        Value = value;
    }

    public static Result<T> Ok(T value) =>
        new(true, value, Error.None);

    public static new Result<T> Fail(Error error) =>
        new(false, default, error);
}
