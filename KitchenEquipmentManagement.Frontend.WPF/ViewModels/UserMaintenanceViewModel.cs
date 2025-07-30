using KitchenEquipmentManagement.Frontend.WPF.ApiServices;
using KitchenEquipmentManagement.Frontend.WPF.Helpers;
using KitchenEquipmentManagement.Frontend.WPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KitchenEquipmentManagement.Frontend.WPF.ViewModels
{
    public class UserMaintenanceViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;

        public ObservableCollection<UserDto> UserDtos { get; set; }
        public ObservableCollection<UserType> UserTypes { get; set; }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private UserDto? _selectedUser;
        public UserDto? SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        public UserMaintenanceViewModel()
        {
            _userService = new UserService();
            _userService.SetToken(TokenStorage.Token);

            UserDtos = new ObservableCollection<UserDto>();
            UserTypes = new ObservableCollection<UserType>(
                Enum.GetValues(typeof(UserType)).Cast<UserType>()
            );

            EditCommand = new RelayCommand(EditUserAsync, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteUserAsync, CanEditOrDelete);

            LoadUsersAsync();
        }

        private async void LoadUsersAsync()
        {
            var userList = await _userService.GetAllUsersAsync();
            if (userList != null)
            {
                UserDtos.Clear();
                foreach (var user in userList)
                {
                    UserDtos.Add(new UserDto() 
                    {  
                        UserId = user.UserId, 
                        FullName = user.FullName,
                        UserName = user.UserName,
                        UserType = user.UserType,
                        EmailAddress = user.EmailAddress
                    });
                }
            }
        }

        private async void EditUserAsync(object? parameter)
        {
            if (SelectedUser == null) return;

            var responseMessage = await _userService.UpdateUserAsync(SelectedUser);

            if (!string.IsNullOrEmpty(responseMessage))
            {
                System.Windows.MessageBox.Show(responseMessage, "Update Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                await ReloadUsersAsync(); 
            }
            else
            {
                System.Windows.MessageBox.Show("Failed to update the user.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private async void DeleteUserAsync(object? parameter)
        {
            if (SelectedUser == null) return;

            bool success = await _userService.DeleteUserAsync(SelectedUser.UserId);
            if (success)
            {
                System.Windows.MessageBox.Show("User DeleteSiteAsync Success", "Update Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                UserDtos.Remove(SelectedUser);
                SelectedUser = null;
            }
            else 
            {
                System.Windows.MessageBox.Show("Failed to delete User.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private bool CanEditOrDelete(object? parameter) => SelectedUser != null;

        private async Task ReloadUsersAsync()
        {
            var userList = await _userService.GetAllUsersAsync();
            if (userList != null)
            {
                UserDtos.Clear();
                foreach (var user in userList)
                {
                    UserDtos.Add(user);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
