using RassApp.Finance.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.Finance.Domain.Entities
{
    public class User : AggregateRoot<Guid>
    {
        private readonly List<RefreshToken> _refreshTokens = new();

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string TenantId { get; private set; }
        public string Role { get; private set; }

        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;

        public User(string name, string email, string passwordHash, string tenantId, string role)
        {
            Id = Guid.NewGuid(); // importante

            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            TenantId = tenantId;
            Role = role;
        }

        public void AddRefreshToken(RefreshToken token)
            => _refreshTokens.Add(token);
    }
}
