using MediatR;
using RassApp.SharedKernel.Common.Results;

public sealed record CreateAccountCommand(string Name)
    : IRequest<Result<Guid>>;