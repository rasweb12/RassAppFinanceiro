using System;
using System.Collections.Generic;
using System.Text;
using RassApp.SharedKernel.Common.Results;

namespace RassApp.Finance.Domain.Errors;

public static class DomainErrors
{
    public static Error AccountNotFound =>
        new("ACCOUNT_NOT_FOUND", "Conta não encontrada.");

    public static Error InvalidAmount =>
        new("INVALID_AMOUNT", "Valor inválido.");

    public static Error Unauthorized =>
        new("UNAUTHORIZED", "Acesso não autorizado.");
}
