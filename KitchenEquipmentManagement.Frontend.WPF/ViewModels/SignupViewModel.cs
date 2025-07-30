using KitchenEquipmentManagement.Frontend.WPF.ApiServices;
using KitchenEquipmentManagement.Frontend.WPF.Helpers;
using KitchenEquipmentManagement.Frontend.WPF.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KitchenEquipmentManagement.Frontend.WPF.ViewModels
{
    public class SignupViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;

        public SignupViewModel()
        {
            _authService = new AuthService();
            RegisterCommand = new RelayCommand(async (_) => await RegisterAsync());
            CancelCommand = new RelayCommand((_) => CloseSignUp());
            IsAdmin = true; // default
        }

        // Properties bound to UI
        public string FirstName { get => _firstName; set { _firstName = value; OnPropertyChanged(nameof(FirstName)); } }
        public string LastName { get => _lastName; set { _lastName = value; OnPropertyChanged(nameof(LastName)); } }
        public string EmailAddress { get => _emailAddress; set { _emailAddress = value; OnPropertyChanged(nameof(EmailAddress)); } }
        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(nameof(UserName)); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); } }

        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                if (value) IsSuperAdmin = false;
                OnPropertyChanged(nameof(IsAdmin));
                OnPropertyChanged(nameof(UserType));
            }
        }

        public bool IsSuperAdmin
        {
            get => _isSuperAdmin;
            set
            {
                _isSuperAdmin = value;
                if (value) IsAdmin = false;
                OnPropertyChanged(nameof(IsSuperAdmin));
                OnPropertyChanged(nameof(UserType));
            }
        }

        public UserType UserType => IsAdmin ? UserType.Admin : UserType.SuperAdmin;

        public ICommand RegisterCommand { get; }
        public ICommand CancelCommand { get; }

        private async Task RegisterAsync()
        {
            var request = new SignupRequest
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                UserName = UserName,
                Password = Password,
                UserType = (int)this.UserType
            };

            var response = await _authService.SignupAsync(request);
            MessageBox.Show(response);
            CloseSignUp();
        }

        private void CloseSignUp()
        {
            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this)?.Close();
        }

        // Backing fields
        private string _firstName;
        private string _lastName;
        private string _emailAddress;
        private string _userName;
        private string _password;
        private bool _isAdmin;
        private bool _isSuperAdmin;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
