using KitchenEquipmentManagement.Application.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Infrastructure.Authorization
{
    public class TokenService : ITokenService
    {
        private readonly ConcurrentDictionary<string, bool> _revokedTokens = new();

        public void RevokeToken(string jti)
        {
            if (!string.IsNullOrEmpty(jti))
            {
                _revokedTokens[jti] = true;
            }
        }

        public bool IsRevoked(string jti)
        {
            return !string.IsNullOrEmpty(jti) && _revokedTokens.ContainsKey(jti);
        }
    }
}
