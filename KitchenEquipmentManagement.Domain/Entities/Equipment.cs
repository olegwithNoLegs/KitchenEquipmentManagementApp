using KitchenEquipmentManagement.Domain.Enums;

namespace KitchenEquipmentManagement.Domain.Models
{

    public class Equipment
    {   
        public int EquipmentId { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string Description{ get; set; } = string.Empty;
        public EquipmentCondition Condition { get; set; }
        public int UserId { get; set; }
        public int? SiteId { get; set; }

        public User User { get; set; }
        public ICollection<RegisteredEquipment> RegisteredEquipments { get; set; } = new List<RegisteredEquipment>();
    }



}
