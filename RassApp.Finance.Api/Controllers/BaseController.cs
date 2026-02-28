using Microsoft.AspNetCore.Mvc;
using RassApp.Finance.Api.Contracts.Common;
using RassApp.SharedKernel.Common.Results;

namespace RassApp.Finance.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.Success)
            return Ok(ApiResponse<T>.Ok(result.Value!));

        return BadRequest(
            ApiResponse<T>.Fail(
                result.Error.Code,
                result.Error.Message));
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.Success)
            return Ok(new { Success = true });

        return BadRequest(new
        {
            Success = false,
            result.Error.Code,
            result.Error.Message
        });
    }
}