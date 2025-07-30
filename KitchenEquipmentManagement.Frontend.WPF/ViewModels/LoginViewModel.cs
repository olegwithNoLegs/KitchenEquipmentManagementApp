
using System.ComponentModel;               // For INotifyPropertyChanged
using System.Linq;                         // For LINQ methods like FirstOrDefault
using System.Threading.Tasks;              // For async/await
using System.Windows;                      // For Application and Window
using System.Windows.Input;                // For ICommand
using KitchenEquipmentManagement.Frontend.WPF.ApiServices;
using KitchenEquipmentManagement.Frontend.WPF.Helpers;   // For RelayCommand
using KitchenEquipmentManagement.Frontend.WPF.Models;    // For LoginRequest
using KitchenEquipmentManagement.Frontend.WPF.ViewModels;
using KitchenEquipmentManagement.Frontend.WPF.Views;     // For AdminView and SignupView


public class LoginViewModel : INotifyPropertyChanged
{
    private readonly AuthService _authService;

    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            if (_username != value)
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            if (_password != value)
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }

    public ICommand LoginCommand { get; }
    public ICommand SignupCommand { get; }

    public LoginViewModel()
    {
        _authService = new AuthService();
        LoginCommand = new RelayCommand(async (_) => await LoginAsync());
        SignupCommand = new RelayCommand(_ => OpenSignupView());
    }

    private async Task LoginAsync()
    {
        var response = await _authService.LoginAsync(new LoginRequest
        {
            Username = Username,
            Password = Password
        });

        if (response != null && !string.IsNullOrEmpty(response.Token) && !string.IsNullOrEmpty(response.UserType))
        {
            var adminView = new AdminView();
            adminView.DataContext = new AdminViewModel(response.Token, response.UserType);
            adminView.Show();
            CloseLoginWindow();
        }
        else
        {
            MessageBox.Show(response?.ErrorMessage ?? "Login failed. Please try again.");
        }
    }

    private void OpenSignupView()
    {
        var signupView = new SignupView();
        signupView.DataContext = new SignupViewModel();
        signupView.Show();
    }

    private void CloseLoginWindow()
    {
        Application.Current.Windows
            .OfType<Window>()
            .FirstOrDefault(w => w.DataContext == this)?.Close();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
