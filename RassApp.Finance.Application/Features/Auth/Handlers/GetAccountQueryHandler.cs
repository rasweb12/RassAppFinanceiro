using MediatR;
using RassApp.Finance.Application.Features.Accounts;
using RassApp.SharedKernel.Common.Results;

public class GetAccountQueryHandler
    : IRequestHandler<GetAccountQuery, Result<AccountDto>>
{
    public Task<Result<AccountDto>> Handle(
        GetAccountQuery request,
        CancellationToken cancellationToken)
    {
        var account = new AccountDto
        {
            Id = request.Id,
            Name = "Conta Exemplo"
        };

        return Task.FromResult(Result<AccountDto>.Ok(account));
    }
}