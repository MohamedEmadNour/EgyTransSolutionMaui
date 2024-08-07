using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace EgyTrans.OwenSystem.CRUDs.TypeVisit
{
    public partial class AddTypeVisitPage : ContentPage, INotifyPropertyChanged
    {
        private string searchText;
        private ClientTypeVisit _clientTypeVisit;
        private readonly IUnitOfWork _unitOfWork;
        private ObservableCollection<ClientTypeVisit> _clientTypeVisitAllData;

        public ClientTypeVisit ClientTypeVisit
        {
            get => _clientTypeVisit;
            set
            {
                _clientTypeVisit = value;
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
                    LoadAllClientTypeVisitData();
                }
            }
        }

        public ObservableCollection<ClientTypeVisit> ClientTypeVisitAllData
        {
            get => _clientTypeVisitAllData;
            set
            {
                if (_clientTypeVisitAllData != value)
                {
                    _clientTypeVisitAllData = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddTypeVisitPage(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _clientTypeVisit = new ClientTypeVisit();
            _clientTypeVisitAllData = new ObservableCollection<ClientTypeVisit>();
            InitializeComponent();
            BindingContext = this;
            LoadAllClientTypeVisitData();
        }

        private async void AddTypeVisitButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_clientTypeVisit.TypeName))
                {
                    await DisplayAlert("Validation Error", "Type Name cannot be empty", "OK");
                    return;
                }

                await _unitOfWork.Repositories<ClientTypeVisit>().AddAsync(_clientTypeVisit);
                await _unitOfWork.CompleteAsync();
                LoadAllClientTypeVisitData();
                await DisplayAlert("Success", $"Type {_clientTypeVisit.TypeName} data has been added successfully", "OK");
                ClientTypeVisit = new ClientTypeVisit(); // Reset form
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void LoadAllClientTypeVisitData()
        {
            var clientTypeVisitDataList = string.IsNullOrWhiteSpace(SearchText)
                ? await _unitOfWork.Repositories<ClientTypeVisit>().GetAllAsync()
                : await _unitOfWork.Repositories<ClientTypeVisit>().GetAllAsync(ctv => ctv.TypeName.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            ClientTypeVisitAllData = new ObservableCollection<ClientTypeVisit>(clientTypeVisitDataList);
        }

        private void OnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            if (_clientTypeVisitAllData != null && _clientTypeVisitAllData.Count > 0)
            {
                if (e.CurrentPosition == -1)
                {
                    // User swiped left from the first item, move to the last item
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        ClientTypeVisitAllDataCarouselView.Position = _clientTypeVisitAllData.Count - 1;
                    });
                }
                else if (e.CurrentPosition == _clientTypeVisitAllData.Count)
                {
                    // User swiped right from the last item, move to the first item
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        ClientTypeVisitAllDataCarouselView.Position = 0;
                    });
                }
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
            LoadAllClientTypeVisitData();
        }

        protected override void OnDisappearing()
        {
            Navigation.PopAsync();
            base.OnDisappearing();
        }

        private void ButtonEditTypeVisitData_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var clientTypeVisit = button?.BindingContext as ClientTypeVisit;
            if (clientTypeVisit != null)
            {
                Navigation.PushAsync(new EditTypeVisitPage(clientTypeVisit, _unitOfWork));
            }
        }
    }
}
