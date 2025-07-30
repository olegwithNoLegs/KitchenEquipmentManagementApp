using KitchenEquipmentManagement.Domain.Enums;

namespace KitchenEquipmentManagement.Application.DTOs
{
    public class SignupRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
