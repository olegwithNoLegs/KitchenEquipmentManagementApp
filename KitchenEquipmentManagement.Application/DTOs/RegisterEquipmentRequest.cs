using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.DTOs
{
    public class RegisterEquipmentRequest
    {
        public int SiteId { get; set; }
        public int EquipmentId { get; set; }
    }
}
