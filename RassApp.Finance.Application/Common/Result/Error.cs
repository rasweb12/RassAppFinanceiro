using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.Finance.Application.Common.Result;

public sealed record Error(
    string Code,
    string Description,
    ErrorType Type)
{
    public static Error None => new(string.Empty, string.Empty, ErrorType.Failure);

    public static Error Validation(string description)
        => new("Validation", description, ErrorType.Validation);

    public static Error NotFound(string description)
        => new("NotFound", description, ErrorType.NotFound);

    public static Error Unauthorized(string description)
        => new("Unauthorized", description, ErrorType.Unauthorized);

    public static Error Forbidden(string description)
        => new("Forbidden", description, ErrorType.Forbidden);

    public static Error Conflict(string description)
        => new("Conflict", description, ErrorType.Conflict);

    public static Error Failure(string description)
        => new("Failure", description, ErrorType.Failure);
}
