using MediatR;
using RassApp.Finance.Application.Features.Accounts;
using RassApp.SharedKernel.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

public record GetAccountQuery(Guid Id)
    : IRequest<Result<AccountDto>>;
