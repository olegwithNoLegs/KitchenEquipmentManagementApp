using KitchenEquipmentManagement.Frontend.WPF.Models; 
using KitchenEquipmentManagement.Frontend.WPF.Helpers;
using KitchenEquipmentManagement.Frontend.WPF.Views;
using System.Windows.Input;
using KitchenEquipmentManagement.Frontend.WPF.ApiServices;

namespace KitchenEquipmentManagement.Frontend.WPF.ViewModels
{
    public class AdminViewModel
    {
        private readonly AuthService _authService;

        public string Token { get; }
        public UserType UserType { get; }

        public ICommand NavigateUsersCommand { get; }
        public ICommand NavigateSitesCommand { get; }
        public ICommand NavigateEquipmentsCommand { get; }
        public ICommand LogoutCommand { get; }

        public bool IsSuperAdmin => UserType == UserType.SuperAdmin;

        public AdminViewModel(string token, string userType)
        {
            Token = token;

            if (!Enum.TryParse(userType, out UserType parsedRole))
            {
                parsedRole = UserType.Admin;
            }
            UserType = parsedRole;

            NavigateUsersCommand = new RelayCommand(OpenUsers);
            NavigateSitesCommand = new RelayCommand(OpenSites);
            NavigateEquipmentsCommand = new RelayCommand(OpenEquipments);
            LogoutCommand = new RelayCommand(Logout);

            _authService = new AuthService();
        }

        private void OpenUsers(object? obj) 
        {
            var userView = new UserMaintenanceView();
            userView.DataContext = new UserMaintenanceViewModel();
            userView.Show();
        }
        private void OpenSites(object? obj) 
        {
            var siteView = new SiteMaintenanceView();
            siteView.DataContext = new SiteMaintenanceViewModel();
            siteView.Show();
        }
        private void OpenEquipments(object? obj) 
        { 
            var equipmentsView = new EquipmentMaintenanceView();
            equipmentsView.DataContext = new EquipmentMaintenanceViewModel();
            equipmentsView.Show();
        }

        private async void Logout(object? obj)
        {
            await _authService.SignoutAsync();

            System.Windows.Application.Current.Windows
                .OfType<System.Windows.Window>()
                .FirstOrDefault(w => w.DataContext == this)?.Close();

            var loginView = new LoginView();
            loginView.Show();
        }
    }
}
