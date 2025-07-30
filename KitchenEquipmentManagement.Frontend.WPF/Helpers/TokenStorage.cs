using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Frontend.WPF.Helpers
{
    public static class TokenStorage
    {
        public static string Token { get; set; }
        public static string UserType { get; set; }

        public static void Clear()
        {
            Token = null;
            UserType = null;
        }
    }
}
