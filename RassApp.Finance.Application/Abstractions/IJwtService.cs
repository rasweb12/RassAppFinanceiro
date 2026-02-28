using System;
using System.Collections.Generic;
using System.Text;
using RassApp.Finance.Domain.Entities;
using RassApp.Security.Models;

namespace RassApp.Finance.Application.Abstractions;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateAccessToken(JwtUser jwtUser);
    string GenerateRefreshToken();
}
