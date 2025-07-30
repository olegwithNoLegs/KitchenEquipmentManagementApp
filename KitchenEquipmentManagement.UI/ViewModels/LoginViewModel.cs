using KitchenEquipmentManagement.Frontend.WPF.Helpers;
using KitchenEquipmentManagement.Frontend.WPF.Services;
using KitchenEquipmentManagement.Frontend.WPF.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _username;
    private string _password;
    private readonly AuthService _authService;

    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand { get; }
    public ICommand SignUpCommand { get; }

    public LoginViewModel()
    {
        _authService = new AuthService();
        LoginCommand = new RelayCommand(async _ => await LoginAsync());
        SignUpCommand = new RelayCommand(_ => OpenSignUp());
    }

    private async Task LoginAsync()
    {
        var result = await _authService.LoginAsync(Username, Password);
        if (result.IsSuccess)
        {
            // Navigate to AdminView
            NavigationService.Navigate(new AdminView());
        }
        else
        {
            MessageBox.Show("Invalid login");
        }
    }

    private void OpenSignUp()
    {
        NavigationService.Navigate(new SignUpView());
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string name = null!) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
