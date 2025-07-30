using KitchenEquipmentManagement.Frontend.WPF.ApiServices;
using KitchenEquipmentManagement.Frontend.WPF.Helpers;
using KitchenEquipmentManagement.Frontend.WPF.Models;
using KitchenEquipmentManagement.Frontend.WPF.Models.KitchenEquipmentManagement.Frontend.WPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace KitchenEquipmentManagement.Frontend.WPF.ViewModels
{
    public class SiteEquipmentMaintenanceViewModel : INotifyPropertyChanged
    {
        private readonly EquipmentService _equipmentService;
        private readonly RegisteredEquipmentService _registerEquipmentService;

        public ObservableCollection<EquipmentDto> UnregisteredEquipments { get; set; }
        public ObservableCollection<EquipmentDto> RegisteredEquipments { get; set; }
        public int SiteId { get; }
        public string SiteDescription { get; }

        public ICommand RegisterEquipmentCommand { get; }
        public ICommand UnregisterEquipmentCommand { get; }

        private EquipmentDto? _selectedUnregisteredEquipment;
        public EquipmentDto? SelectedEquipmentToRegister
        {
            get => _selectedUnregisteredEquipment;
            set { _selectedUnregisteredEquipment = value; OnPropertyChanged(); }
        }

        private EquipmentDto? _selectedRegisteredEquipment;
        public EquipmentDto? SelectedEquipmentToUnregister
        {
            get => _selectedRegisteredEquipment;
            set { _selectedRegisteredEquipment = value; OnPropertyChanged(); }
        }

        public SiteEquipmentMaintenanceViewModel(int siteId, string siteDescription)
        {
            _equipmentService = new EquipmentService();
            _equipmentService.SetToken(TokenStorage.Token);
            _registerEquipmentService = new RegisteredEquipmentService();
            _registerEquipmentService.SetToken(TokenStorage.Token);
            UnregisteredEquipments = new ObservableCollection<EquipmentDto>();
            RegisteredEquipments = new ObservableCollection<EquipmentDto>();
            RegisterEquipmentCommand = new RelayCommand(RegisterEquipmentAsync, CanRegister);
            UnregisterEquipmentCommand = new RelayCommand(UnregisterEquipmentAsync, CanUnregister);

            SiteId = siteId;
            SiteDescription = siteDescription;
            LoadEquipmentsAsync();
        }

        private async void LoadEquipmentsAsync()
        {
            var unregisteredEquipments = await _equipmentService.GetUnregisteredEquipmentsAsync();
            if (unregisteredEquipments != null)
            {
                UnregisteredEquipments.Clear();
                foreach (var equipment in unregisteredEquipments)
                {
                    UnregisteredEquipments.Add(new EquipmentDto()
                    {
                        EquipmentId = equipment.EquipmentId,
                        SerialNumber = equipment.SerialNumber,
                        Description = equipment.Description,
                        Condition = equipment.Condition
                    });
                }
            }

            var registeredEquipments = await _equipmentService.GetRegisteredEquipmentsAsync(SiteId);
            if (registeredEquipments != null)
            {
                RegisteredEquipments.Clear();
                foreach (var equipment in registeredEquipments)
                {
                    RegisteredEquipments.Add(new EquipmentDto()
                    {
                        EquipmentId = equipment.EquipmentId,
                        SerialNumber = equipment.SerialNumber,
                        Description = equipment.Description,
                        Condition= equipment.Condition
                    });
                }
            }
        }

        private async void RegisterEquipmentAsync(object? parameter)
        {
            if (SelectedEquipmentToRegister == null) return;

            var request = new RegisterEquipmentRequest()
            {
                SiteId = SiteId,
                EquipmentId = SelectedEquipmentToRegister.EquipmentId
            };

            var response = await _registerEquipmentService.RegisterEquipment(request);

            if (!string.IsNullOrEmpty(response))
            {
                System.Windows.MessageBox.Show(response, "Register Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                LoadEquipmentsAsync();
            }
            else
            {
                System.Windows.MessageBox.Show(response, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private async void UnregisterEquipmentAsync(object? parameter)
        {
            if (SelectedEquipmentToUnregister == null) return;


            var response = await _registerEquipmentService.UnregisterEquipment(SelectedEquipmentToUnregister.EquipmentId);

            if (response)
            {
                System.Windows.MessageBox.Show($"Equipment {SelectedEquipmentToUnregister.EquipmentId} is unregistered from {SiteId}", "Delete Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                LoadEquipmentsAsync();
            }
            else
            {
                System.Windows.MessageBox.Show("Unregistering equipment from Site failed.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private bool CanRegister(object? parameter) => SelectedEquipmentToRegister != null;
        private bool CanUnregister(object? parameter) => SelectedEquipmentToUnregister != null;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
