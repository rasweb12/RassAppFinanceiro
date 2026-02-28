using RassApp.Finance.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.Finance.Domain.Entities
{
    public class RefreshToken : Entity<Guid>
    {
        public string Token { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool Revoked { get; private set; }

        public RefreshToken(string token, DateTime expirationDate)
        {
            Token = token;
            ExpirationDate = expirationDate;
        }

        public void Revoke() => Revoked = true;
    }
}
