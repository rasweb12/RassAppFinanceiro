using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.Finance.Application.Common.Result;

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Unauthorized = 3,
    Forbidden = 4,
    Conflict = 5
}
