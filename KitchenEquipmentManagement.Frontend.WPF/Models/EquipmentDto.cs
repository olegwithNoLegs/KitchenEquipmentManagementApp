namespace KitchenEquipmentManagement.Frontend.WPF.Models
{
    public class EquipmentDto
    {
        public int EquipmentId { get; set; }
        public string? SerialNumber { get; set; }
        public string Description { get; set; }
        public EquipmentCondition Condition { get; set; }
    }
}
