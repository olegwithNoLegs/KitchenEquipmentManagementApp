namespace KitchenEquipmentManagement.Domain.Models
{
    public class RegisteredEquipment
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int SiteId { get; set; }

        public Equipment Equipment { get; set; }
        public Site Site { get; set; }
    }

}
