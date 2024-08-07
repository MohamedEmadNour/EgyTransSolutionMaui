using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.Services;
using System.Linq;
using Microsoft.Maui.Controls;

namespace EgyTrans.OwenSystem.CRUDs.Travel
{
    public partial class EditTravelDataPage : ContentPage
    {
        private readonly TravelDataManager _travelDataManager;
        private readonly TravelData _travelData;

        private List<CarData> _selectedCars = new List<CarData>();
        private List<DriverData> _selectedDrivers = new List<DriverData>();
        private List<SupplierData> _selectedSuppliers = new List<SupplierData>();
        private List<TourGuideClass> _selectedGuides = new List<TourGuideClass>();
        private List<TravelInfo> _selectedTravelInfos = new List<TravelInfo>();

        public EditTravelDataPage(TravelData travelData, TravelDataManager travelDataManager)
        {
            InitializeComponent();
            _travelData = travelData;
            _travelDataManager = travelDataManager;
            BindingContext = this;
            InitializeData();
        }

        private async void InitializeData()
        {
            ClientPicker.ItemsSource = await _travelDataManager.GetAllClientsAsync();
            TypePicker.ItemsSource = await _travelDataManager.GetAllClientTypeVisitsAsync();
            ClientPicker.SelectedItem = await _travelDataManager.GetClientByIdAsync(_travelData.ClientID);
            TypePicker.SelectedItem = await _travelDataManager.GetClientTypeVisitByIdAsync(_travelData.TypeID);
            TransferDatePicker.Date = _travelData.TransferDateTime;
            NetCostEntry.Text = _travelData.NetCost.ToString();
            SellingPriceEntry.Text = _travelData.SellingPrice.ToString();
            ProfitEntry.Text = _travelData.Profit.ToString();
            StatusPicker.SelectedItem = _travelData.Status;

            _selectedCars = _travelData.Cars.ToList();
            _selectedDrivers = _travelData.Drivers.ToList();
            _selectedSuppliers = _travelData.Suppliers.ToList();
            _selectedTravelInfos = _travelData.TravelInfos.ToList();

            SelectedCarsCollectionView.ItemsSource = _selectedCars;
            SelectedDriversCollectionView.ItemsSource = _selectedDrivers;
            SelectedSuppliersCollectionView.ItemsSource = _selectedSuppliers;
            SelectedTravelInfosCollectionView.ItemsSource = _selectedTravelInfos;
            SelectedTourGuidesCollectionView.ItemsSource = _selectedGuides;
        }

        private async void OnSelectCarsClicked(object sender, EventArgs e)
        {
            var allCars = await _travelDataManager.GetAllCarsAsync();
            var selectedCars = await DisplayActionSheet("Select Cars", "Cancel", null,
                allCars.Select(c => c.CarPrand).ToArray());

            if (selectedCars != "Cancel")
            {
                _selectedCars = allCars.Where(c => selectedCars.Contains(c.CarPrand)).ToList();
                SelectedCarsCollectionView.ItemsSource = _selectedCars;
            }
        }

        private async void OnSelectDriversClicked(object sender, EventArgs e)
        {
            var allDrivers = await _travelDataManager.GetAllDriversAsync();
            var selectedDrivers = await DisplayActionSheet("Select Drivers", "Cancel", null,
                allDrivers.Select(d => d.DriverName).ToArray());

            if (selectedDrivers != "Cancel")
            {
                _selectedDrivers = allDrivers.Where(d => selectedDrivers.Contains(d.DriverName)).ToList();
                SelectedDriversCollectionView.ItemsSource = _selectedDrivers;
            }
        }

        private async void OnSelectSuppliersClicked(object sender, EventArgs e)
        {
            var allSuppliers = await _travelDataManager.GetAllSuppliersAsync();
            var selectedSuppliers = await DisplayActionSheet("Select Suppliers", "Cancel", null,
                allSuppliers.Select(s => s.SupplierName).ToArray());

            if (selectedSuppliers != "Cancel")
            {
                _selectedSuppliers = allSuppliers.Where(s => selectedSuppliers.Contains(s.SupplierName)).ToList();
                SelectedSuppliersCollectionView.ItemsSource = _selectedSuppliers;
            }
        }

        private async void OnSelectTourGuideClicked(object sender, EventArgs e)
        {
            var allTourGuides = await _travelDataManager.GetAllselectedallTourGuidesAsync();
            var selectedallTourGuides = await DisplayActionSheet("Select Tour Guide", "Cancel", null,
                allTourGuides.Select(s => s.GuideName).ToArray());

            if (selectedallTourGuides != "Cancel")
            {
                _selectedGuides = allTourGuides.Where(s => selectedallTourGuides.Contains(s.GuideName)).ToList();
                SelectedTourGuidesCollectionView.ItemsSource = _selectedGuides;
            }
        }

        private async void OnSelectTravelInfoClicked(object sender, EventArgs e)
        {
            var transferFrom = await DisplayPromptAsync("Transfer From", "Enter transfer origin");
            var transferTo = await DisplayPromptAsync("Transfer To", "Enter transfer destination");
            var atDay = await DisplayPromptAsync("Transfer Date", "Enter transfer date (yyyy-MM-dd)", keyboard: Keyboard.Text);

            if (!string.IsNullOrEmpty(transferFrom) && !string.IsNullOrEmpty(transferTo) && !string.IsNullOrEmpty(atDay))
            {
                if (DateTime.TryParse(atDay, out DateTime parsedDate))
                {
                    var travelInfo = new TravelInfo
                    {
                        TransferFrom = transferFrom,
                        TransferTo = transferTo,
                        AtDay = parsedDate
                    };

                    _selectedTravelInfos.Add(travelInfo);
                    SelectedTravelInfosCollectionView.ItemsSource = null;
                    SelectedTravelInfosCollectionView.ItemsSource = _selectedTravelInfos;
                }
                else
                {
                    await DisplayAlert("Error", "Invalid date format. Please use yyyy-MM-dd.", "OK");
                }
            }
        }

        private async void OnSaveTravelDataClicked(object sender, EventArgs e)
        {
            if (ClientPicker.SelectedItem == null || TypePicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Please select a client and a type.", "OK");
                return;
            }

            if (!decimal.TryParse(NetCostEntry.Text, out decimal netCost) ||
                !decimal.TryParse(SellingPriceEntry.Text, out decimal sellingPrice) ||
                !decimal.TryParse(ProfitEntry.Text, out decimal profit))
            {
                await DisplayAlert("Error", "Please enter valid numeric values for Net Cost, Selling Price, and Profit.", "OK");
                return;
            }

            _travelData.ClientID = ((ClientData)ClientPicker.SelectedItem).ClientID;
            _travelData.TypeID = ((ClientTypeVisit)TypePicker.SelectedItem).TypeID;
            _travelData.TransferDateTime = TransferDatePicker.Date;
            _travelData.NetCost = netCost;
            _travelData.SellingPrice = sellingPrice;
            _travelData.Profit = profit;
            _travelData.Status = StatusPicker.SelectedItem?.ToString();
            _travelData.Cars = _selectedCars;
            _travelData.Drivers = _selectedDrivers;
            _travelData.Suppliers = _selectedSuppliers;
            _travelData.TravelInfos = _selectedTravelInfos;
            _travelData.TourGuides = _selectedGuides;

            try
            {
                await _travelDataManager.UpdateTravelDataAsync(_travelData);
                await DisplayAlert("Success", "Travel data updated successfully", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to update travel data: {ex.Message}", "OK");
            }
        }
    }
}
