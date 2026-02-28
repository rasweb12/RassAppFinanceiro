using MediatR;
using RassApp.Finance.Application.Abstractions;
using RassApp.Finance.Domain.Entities;
using RassApp.SharedKernel.Common.Results;

namespace RassApp.Finance.Application.Features.Accounts.CreateAccount;

public sealed class CreateAccountHandler
    : IRequestHandler<CreateAccountCommand, Result<Guid>>
{
    private readonly IRepository<Account> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountHandler(
        IRepository<Account> repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateAccountCommand request,
        CancellationToken cancellationToken)
    {
        var account = new Account(request.Name);

        await _repository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Ok(account.Id);
    }
}