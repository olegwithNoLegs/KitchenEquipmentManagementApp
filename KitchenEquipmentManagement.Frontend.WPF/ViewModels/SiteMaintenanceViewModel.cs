using KitchenEquipmentManagement.Frontend.WPF.ApiServices;
using KitchenEquipmentManagement.Frontend.WPF.Helpers;
using KitchenEquipmentManagement.Frontend.WPF.Models;
using KitchenEquipmentManagement.Frontend.WPF.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace KitchenEquipmentManagement.Frontend.WPF.ViewModels
{
    public class SiteMaintenanceViewModel : INotifyPropertyChanged
    {
        private readonly SiteService _siteService;
        public ObservableCollection<SiteDto> Sites { get; set; }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditSiteEquipmentCommand { get; }

        private SiteDto? _selectedSite;
        public SiteDto? SelectedSite 
        {
            get => _selectedSite;
            set { _selectedSite = value; OnPropertyChanged(); }
        }

        public SiteMaintenanceViewModel()
        {
            _siteService = new SiteService();
            _siteService.SetToken(TokenStorage.Token);
            Sites = new ObservableCollection<SiteDto>();

            AddCommand = new RelayCommand(AddSiteAsync, CanAddSite);
            EditCommand = new RelayCommand(EditSiteAsync, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteSiteAsync, CanEditOrDelete);
            EditSiteEquipmentCommand = new RelayCommand(EditSiteEquipment, CanEditOrDelete);

            LoadSitesAsync();
        }

        private async void LoadSitesAsync()
        {
            var siteList = await _siteService.GetAllSitesAsync();
            if (siteList != null)
            {
                Sites.Clear();
                foreach (var site in siteList)
                {
                    Sites.Add(new SiteDto()
                    {
                        SiteId = site.SiteId,
                        Description = site.Description,
                        Active = site.Active
                    });
                }
            }
        }

        private async void AddSiteAsync(object? parameter)
        {
            if (SelectedSite == null) return;

            var response = await _siteService.AddSiteAsync(SelectedSite);

            if (!string.IsNullOrEmpty(response)) 
            {
                System.Windows.MessageBox.Show(response, "RegisterEquipmentAsync Site Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                LoadSitesAsync();
            }

            else
            {
                System.Windows.MessageBox.Show(response, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private async void EditSiteAsync(object? parameter)
        {
            if (SelectedSite == null) return;

            var response = await _siteService.UpdateSiteAsync(SelectedSite);

            if (!string.IsNullOrEmpty(response))
            {
                System.Windows.MessageBox.Show(response, "Update Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                LoadSitesAsync(); 
            }
            else
            {
                System.Windows.MessageBox.Show("Failed to update the site.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private async void DeleteSiteAsync(object? parameter)
        {
            if (SelectedSite == null) return;

            var success = await _siteService.DeleteSiteAsync(SelectedSite.SiteId);
            if (success)
            {
                System.Windows.MessageBox.Show("Site Deletion Success", "Update Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                Sites.Remove(SelectedSite);
                SelectedSite = null;
            }
            else
            {
                System.Windows.MessageBox.Show("Failed to delete Site.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }


        private void EditSiteEquipment(object? parameter)
        {
            if (SelectedSite == null) return;

            var siteEquipmentView = new SiteEquipmentMaintenanceView();
            siteEquipmentView.DataContext = new SiteEquipmentMaintenanceViewModel(SelectedSite.SiteId, SelectedSite.Description);
            siteEquipmentView.Show();
        }


        private bool CanEditOrDelete(object? parameter) => SelectedSite?.SiteId != 0 || SelectedSite == null;

        private bool CanAddSite(object? parameter) => SelectedSite?.SiteId == 0;


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
