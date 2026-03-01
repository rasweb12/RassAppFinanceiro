using MediatR;
using RassApp.Finance.Application.Abstractions;
using RassApp.Finance.Application.Common.Result;
using RassApp.Finance.Application.Features.Auth.Commands;
using RassApp.Finance.Application.Features.Auth.DTOs;
using RassApp.Finance.Domain.Entities;
using RassApp.Security.Abstractions;
using RassApp.Security.Models;
using RassApp.Security.Services;
using RassApp.SharedKernel.Abstractions.Persistence;


namespace RassApp.Finance.Application.Features.Auth.Handlers;

public sealed class LoginHandler
    : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IUserRepository _repository;
    private readonly IJwtService _jwt;

    public LoginHandler(
        IUserRepository repository,
        IJwtService jwt)
    {
        _repository = repository;
        _jwt = jwt;
    }

    public async Task<Result<LoginResponse>> Handle(
        LoginCommand request,
        CancellationToken ct)
    {
        var user = await _repository.GetByEmailAsync(request.Email);

        if (user is null)
            return Result.Failure<LoginResponse>(
                Error.Unauthorized("Invalid credentials"));

        // TODO: validar senha corretamente (hash seguro)

        // 🔐 Converter User (Domain) → JwtUser (Security)
        var jwtUser = new JwtUser
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            TenantId = user.TenantId
        };

        var accessToken = _jwt.GenerateAccessToken(jwtUser);
        var refreshToken = _jwt.GenerateRefreshToken();

        user.AddRefreshToken(
            new RefreshToken(
                refreshToken,
                DateTime.UtcNow.AddDays(7)
            )
        );

        await _repository.UnitOfWork.SaveChangesAsync(ct);

        return Result.Success(
            new LoginResponse(accessToken, refreshToken));
    }
}