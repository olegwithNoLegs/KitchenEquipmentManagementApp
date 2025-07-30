using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Frontend.WPF.Models
{
    namespace KitchenEquipmentManagement.Frontend.WPF.Models
    {
        public class SiteEquipmentDto
        {
            public int Id { get; set; }

            public int SiteId { get; set; }

            public int EquipmentId { get; set; }

            public string EquipmentName { get; set; } = string.Empty;

            public EquipmentCondition Condition { get; set; }
        }
    }

}
