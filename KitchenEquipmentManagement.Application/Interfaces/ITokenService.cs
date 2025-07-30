using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.Interfaces
{
    public interface ITokenService
    {
        void RevokeToken(string jti);
        bool IsRevoked(string jti);
    }
}
