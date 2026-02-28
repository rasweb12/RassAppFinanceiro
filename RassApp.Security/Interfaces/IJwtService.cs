using RassApp.Security.Models;

namespace RassApp.Security.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(JwtUser user);
    string GenerateRefreshToken();
}