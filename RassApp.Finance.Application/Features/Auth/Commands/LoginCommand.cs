using MediatR;
using RassApp.Finance.Application.Common.Result;
using RassApp.Finance.Application.Features.Auth.DTOs;

namespace RassApp.Finance.Application.Features.Auth.Commands;

public sealed record LoginCommand(
    string Email,
    string Password
) : IRequest<Result<LoginResponse>>;