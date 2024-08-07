using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace EgyTrans.OwenSystem.CRUDs.Supplier
{
    public partial class AddSupplierPage : ContentPage, INotifyPropertyChanged
    {
        private string searchText;
        private SupplierData _supplierData;
        private readonly IUnitOfWork _unitOfWork;
        private ObservableCollection<SupplierData> _supplierAllData;

        public SupplierData supplierData
        {
            get => _supplierData;
            set
            {
                _supplierData = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    LoadAllSupplierData();
                }
            }
        }

        public ObservableCollection<SupplierData> supplierAllData
        {
            get => _supplierAllData;
            set
            {
                if (_supplierAllData != value)
                {
                    _supplierAllData = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddSupplierPage(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _supplierData = new SupplierData();
            _supplierAllData = new ObservableCollection<SupplierData>();
            InitializeComponent();
            BindingContext = this;
            LoadAllSupplierData();
        }

        private async void AddsupplierDataButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_supplierData.SupplierName))
                {
                    await DisplayAlert("Validation Error", "Supplier Name cannot be empty", "OK");
                    return;
                }

                await _unitOfWork.Repositories<SupplierData>().AddAsync(_supplierData);
                await _unitOfWork.CompleteAsync();
                LoadAllSupplierData();
                await DisplayAlert("Success", $"Supplier {_supplierData.SupplierName} data has been added successfully", "OK");
                supplierData = new SupplierData(); // Reset form
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void LoadAllSupplierData()
        {
            var supplierDataList = string.IsNullOrWhiteSpace(SearchText)
                ? await _unitOfWork.Repositories<SupplierData>().GetAllAsync()
                : await _unitOfWork.Repositories<SupplierData>().GetAllAsync(s => s.SupplierName.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            supplierAllData = new ObservableCollection<SupplierData>(supplierDataList);
        }

        private void OnEditSupplierClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var supplier = button?.CommandParameter as SupplierData;
            if (supplier != null)
            {
                Navigation.PushAsync(new EditSupplierPage(supplier, _unitOfWork));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadAllSupplierData();
        }

        protected override void OnDisappearing()
        {
            Navigation.PopAsync();
            base.OnDisappearing();
        }
    }
}
