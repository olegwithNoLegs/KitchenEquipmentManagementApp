using KitchenEquipmentManagement.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string username, string role);
    }
}
