using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.SharedKernel.Common.Results;

public class Error
{
    public string Code { get; }
    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static Error None => new(string.Empty, string.Empty);
}
