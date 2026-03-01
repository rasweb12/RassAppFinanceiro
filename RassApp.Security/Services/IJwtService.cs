using RassApp.Security.Models;

namespace RassApp.Security.Abstractions;

public interface IJwtService
{
    string GenerateAccessToken(JwtUser user);
    string GenerateRefreshToken();
}