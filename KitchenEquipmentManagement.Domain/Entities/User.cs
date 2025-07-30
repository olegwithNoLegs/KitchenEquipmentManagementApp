using KitchenEquipmentManagement.Domain.Enums;

namespace KitchenEquipmentManagement.Domain.Models
{


    public class User
    {
        public int UserId { get; set; }
        public UserType UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<Site> Sites { get; set; }
        public ICollection<Equipment> Equipments { get; set; }
    }



}
