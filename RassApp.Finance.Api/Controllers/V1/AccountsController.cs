using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RassApp.Finance.Application.Features.Accounts;
using RassApp.Finance.Application.Features.Accounts.CreateAccount;

namespace RassApp.Finance.Api.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/accounts")]
[ApiVersion("1.0")]
public class AccountsController : BaseController
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAccountCommand command)
    {
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetAccountQuery(id));
        return HandleResult(result);
    }
}