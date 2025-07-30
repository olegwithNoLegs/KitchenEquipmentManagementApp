using KitchenEquipmentManagement.Frontend.WPF.Views;
using System.Windows;

namespace KitchenEquipmentManagement.Frontend.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var loginView = new LoginView();
            loginView.Show();
        }
    }
}
