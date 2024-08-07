using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EgyTrans.OwenSystem.CRUDs.Cars
{
    public partial class AddCarDataPage : ContentPage, INotifyPropertyChanged
    {
        private CarData _carData;
        private readonly IUnitOfWork _unitOfWork;
        private string _searchText;

        public CarData CarData
        {
            get => _carData;
            set
            {
                _carData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CarData> Cars { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterCars();
            }
        }

        public AddCarDataPage(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _carData = new CarData();
            Cars = new ObservableCollection<CarData>();
            BindingContext = this;
            LoadData();
        }

        private async void LoadData()
        {
            var cars = await _unitOfWork.Repositories<CarData>().GetAllAsync();
            Cars = new ObservableCollection<CarData>(cars);
            OnPropertyChanged(nameof(Cars));
        }

        private async void AddCarButton_Clicked(object sender, EventArgs e)
        {
            await _unitOfWork.Repositories<CarData>().AddAsync(_carData);
            await _unitOfWork.CompleteAsync();
            LoadData();
            await DisplayAlert("Success", $"Car {_carData.CarPrand} {_carData.CarModel} has been added successfully", "OK");

            CarData = new CarData();
        }

        private void FilterCars()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadData();
            }
            else
            {
                var filteredCars = Cars.Where(c =>
                    c.CarPrand.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    c.CarModel.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    c.CarNum.ToString().Contains(SearchText)).ToList();
                Cars = new ObservableCollection<CarData>(filteredCars);
                OnPropertyChanged(nameof(Cars));
            }
        }

        private async void OnEditCarClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var car = button?.CommandParameter as CarData;
            if (car != null)
            {
                await Navigation.PushAsync(new EditCarDataPage(_unitOfWork, car));
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}