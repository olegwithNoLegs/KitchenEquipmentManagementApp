using KitchenEquipmentManagement.Domain.Enums;

namespace KitchenEquipmentManagement.Application.DTOs
{
    public class EquipmentDto
    {
        public int EquipmentId { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public EquipmentCondition Condition { get; set; }
    }
}
