namespace RassApp.Finance.Application.Features.Auth.DTOs;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken
);