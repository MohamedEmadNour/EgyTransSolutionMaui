using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace EgyTrans.OwenSystem.Pages
{
    public partial class HomePage : ContentPage, INotifyPropertyChanged
    {
        private readonly TravelDataManager _travelDataManager;
        private ObservableCollection<TravelData> _travelDataList;
        private string _searchText;

        public ObservableCollection<TravelData> TravelDataList
        {
            get => _travelDataList;
            set
            {
                _travelDataList = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SearchTravelData();
            }
        }

        public ICommand RefreshCommand { get; }

        public HomePage(TravelDataManager travelDataManager)
        {
            InitializeComponent();
            _travelDataManager = travelDataManager;
            BindingContext = this;

            RefreshCommand = new Command(async () => await LoadTravelDataAsync());

            // Load data when the page appears
            Appearing += async (s, e) => await LoadTravelDataAsync();
        }

        private async Task LoadTravelDataAsync()
        {
            try
            {
                var travelData = await _travelDataManager.GetAllTravelDataAsync();
                TravelDataList = new ObservableCollection<TravelData>(travelData);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load travel data: {ex.Message}", "OK");
            }
        }

        private async void SearchTravelData()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadTravelDataAsync();
            }
            else
            {
                var allTravelData = await _travelDataManager.GetAllTravelDataAsync();
                foreach (var travel in allTravelData)
                {
                    var client = await _travelDataManager.GetClientByIdAsync(travel.ClientID);
                    var type = await _travelDataManager.GetClientTypeVisitByIdAsync(travel.TypeID);
                    travel.Client.ClientName = client?.ClientName;
                    travel.Type.TypeName = type?.TypeName;
                }

                TravelDataList = new ObservableCollection<TravelData>(allTravelData);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}