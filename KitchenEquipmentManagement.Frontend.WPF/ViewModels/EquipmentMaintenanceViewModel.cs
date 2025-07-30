using KitchenEquipmentManagement.Frontend.WPF.ApiServices;
using KitchenEquipmentManagement.Frontend.WPF.Helpers;
using KitchenEquipmentManagement.Frontend.WPF.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KitchenEquipmentManagement.Frontend.WPF.ViewModels
{
    public class EquipmentMaintenanceViewModel : INotifyPropertyChanged
    {
        private readonly EquipmentService _equipmentService;

        public ObservableCollection<EquipmentDto> EquipmentDtos { get; set; } = new();
        public ObservableCollection<EquipmentCondition> EquipmentConditions { get; set; }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private EquipmentDto? _selectedEquipment;
        public EquipmentDto? SelectedEquipment
        {
            get => _selectedEquipment;
            set { _selectedEquipment = value; OnPropertyChanged(); }
        }

        private EquipmentCondition _selectedCondition;
        public EquipmentCondition SelectedCondition
        {
            get => _selectedCondition;
            set { _selectedCondition = value; OnPropertyChanged(); }
        }

        public EquipmentMaintenanceViewModel()
        {
            _equipmentService = new EquipmentService();
            _equipmentService.SetToken(TokenStorage.Token);

            EquipmentConditions = new ObservableCollection<EquipmentCondition>(
                Enum.GetValues(typeof(EquipmentCondition)).Cast<EquipmentCondition>()
            );

            AddCommand = new RelayCommand(AddEquipment, CanAdd);
            EditCommand = new RelayCommand(EditEquipment, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteEquipment, CanEditOrDelete);

            LoadEquipmentsAsync();
        }


        private async void AddEquipment(object? parameter)
        {
            if (SelectedEquipment == null) return;

            SelectedEquipment.Condition = SelectedCondition;

            var response = await _equipmentService.AddEquipmentAsync(SelectedEquipment);

            if (!string.IsNullOrEmpty(response))
            {
                System.Windows.MessageBox.Show(response, "Update Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                LoadEquipmentsAsync(); 
            }
            else
            {
                System.Windows.MessageBox.Show("Failed to add equipment.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }



        private async void EditEquipment(object? parameter)
        {
            if (SelectedEquipment == null) return;


            var response = await _equipmentService.UpdateEquipmentAsync(SelectedEquipment);

            if (!string.IsNullOrEmpty(response))
            {
                System.Windows.MessageBox.Show(response, "Update Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                LoadEquipmentsAsync(); // Refresh the grid
            }
            else
            {
                System.Windows.MessageBox.Show("Failed to update the equipment.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private async void DeleteEquipment(object? parameter)
        {
            if (SelectedEquipment == null) return;

            var success = await _equipmentService.DeleteEquipmentAsync(SelectedEquipment.EquipmentId);
            if (success)
            {
                System.Windows.MessageBox.Show("Equipment Deletion Success", "Update Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                EquipmentDtos.Remove(SelectedEquipment);
                SelectedEquipment = null;
            }
            else 
            {
                System.Windows.MessageBox.Show("Failed to delete Equipment.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
        private async void LoadEquipmentsAsync()
        {
            var equipmentList = await _equipmentService.GetAllEquipmentAsync();
            if (equipmentList != null)
            {
                EquipmentDtos.Clear();
                foreach (var equipment in equipmentList)
                    EquipmentDtos.Add(equipment);
            }
        }

        private bool CanEditOrDelete(object? parameter) => SelectedEquipment?.EquipmentId != 0 || SelectedEquipment == null;
        private bool CanAdd(object? parameter) => SelectedEquipment?.EquipmentId == 0;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
