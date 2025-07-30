using KitchenEquipmentManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Application.DTOs
{
    public class EquipmentRequest
    {
        public string Description { get; set; }
        public EquipmentCondition Condition { get; set; }
    }
}
