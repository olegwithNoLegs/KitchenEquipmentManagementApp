namespace KitchenEquipmentManagement.Domain.Models
{

    public class Site
    {
        public int SiteId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public User User { get; set; }
        public ICollection<Equipment> Equipments { get; set; }
        public ICollection<RegisteredEquipment> RegisteredEquipments { get; set; }
    }


}
