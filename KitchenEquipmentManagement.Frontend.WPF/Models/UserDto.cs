namespace KitchenEquipmentManagement.Frontend.WPF.Models
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public UserType UserType { get; set; }
    }
}
