
using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.Services;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using System.Linq;

namespace EgyTrans.OwenSystem.CRUDs.Travel
{

    public partial class AddTravelDataPage : ContentPage
    {
        private readonly INotificationManagerService notificationManager;
        private readonly TravelDataManager _travelDataManager;

        private List<CarData> _selectedCars = new List<CarData>();
        private List<DriverData> _selectedDrivers = new List<DriverData>();
        private List<SupplierData> _selectedSuppliers = new List<SupplierData>();
        private List<TravelInfo> _selectedTravelInfos = new List<TravelInfo>();
        private List<TourGuideClass> _selectedTourGuides = new List<TourGuideClass>();
        public AddTravelDataPage(TravelDataManager travelDataManager)
        {
            InitializeComponent();
            _travelDataManager = travelDataManager;
            BindingContext = this;
            notificationManager = Application.Current?.MainPage?.Handler?.MauiContext?.Services.GetService<INotificationManagerService>();
            InitializeData();
        }

        private async void InitializeData()
        {
            ClientPicker.ItemsSource = await _travelDataManager.GetAllClientsAsync();
            TypePicker.ItemsSource = await _travelDataManager.GetAllClientTypeVisitsAsync();
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
                _selectedTourGuides = allTourGuides.Where(s => selectedallTourGuides.Contains(s.GuideName)).ToList();
                SelectedTourGuidesCollectionView.ItemsSource = _selectedTourGuides;
            }
        }
        private async void OnSelectTravelInfoClicked(object sender, EventArgs e)
        {
            var transferFrom = await DisplayPromptAsync("Transfer From", "Enter transfer origin");
            var transferTo = await DisplayPromptAsync("Transfer To", "Enter transfer destination");
            var atDay = await DisplayPromptAsync("Transfer Date", "Enter transfer date (yyyy-MM-dd)", keyboard: Keyboard.Text );

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

            var travelData = new TravelData
            {
                ClientID = ((ClientData)ClientPicker.SelectedItem).ClientID,
                TypeID = ((ClientTypeVisit)TypePicker.SelectedItem).TypeID,
                TransferDateTime = TransferDatePicker.Date,
                NetCost = netCost,
                SellingPrice = sellingPrice,
                Profit = profit,
                Status = StatusPicker.SelectedItem?.ToString(),
                Cars = _selectedCars,
                Drivers = _selectedDrivers,
                Suppliers = _selectedSuppliers,
                TravelInfos = _selectedTravelInfos
            };

            try
            {
                await _travelDataManager.SaveTravelDataAsync(travelData);
                await DisplayAlert("Success", "Travel data saved successfully", "OK");
                var client = (ClientData)ClientPicker.SelectedItem;
                foreach (var travelInfo in _selectedTravelInfos)
                {
                    var description = $"Travel today for {client.ClientName} from {travelInfo.TransferFrom} to {travelInfo.TransferTo}";
                    var title = "Travel Reminder";
                    notificationManager.SendNotification(title, description, travelInfo.AtDay.AddHours(-9));
                }
                
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to save travel data: {ex.Message}", "OK");
            }
        }





    }
}
