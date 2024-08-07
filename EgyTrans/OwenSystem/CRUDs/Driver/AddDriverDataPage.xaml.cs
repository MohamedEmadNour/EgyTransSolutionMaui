namespace EgyTrans.OwenSystem.CRUDs.Driver;
using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

public partial class AddDriverDataPage : ContentPage, INotifyPropertyChanged
{
    private DriverData _driverData;
    private readonly IUnitOfWork _unitOfWork;
    private string _searchText;

    public DriverData DriverData
    {
        get => _driverData;
        set
        {
            _driverData = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<DriverData> Drivers { get; set; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            FilterDrivers();
        }
    }

    public AddDriverDataPage(IUnitOfWork unitOfWork)
    {
        InitializeComponent();
        _unitOfWork = unitOfWork;
        _driverData = new DriverData();
        Drivers = new ObservableCollection<DriverData>();
        BindingContext = this;
        LoadData();
    }

    private async void LoadData()
    {
        var drivers = await _unitOfWork.Repositories<DriverData>().GetAllAsync();
        Drivers = new ObservableCollection<DriverData>(drivers);
        OnPropertyChanged(nameof(Drivers));
    }

    private async void AddDriverButton_Clicked(object sender, EventArgs e)
    {
        await _unitOfWork.Repositories<DriverData>().AddAsync(_driverData);
        await _unitOfWork.CompleteAsync();
        LoadData();
        await DisplayAlert("Success", $"Driver {_driverData.DriverName} has been added successfully", "OK");

        DriverData = new DriverData();
    }

    private void FilterDrivers()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            LoadData();
        }
        else
        {
            var filteredDrivers = Drivers.Where(d =>
                d.DriverName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                d.DriverPhone.ToString().Contains(SearchText)).ToList();
            Drivers = new ObservableCollection<DriverData>(filteredDrivers);
            OnPropertyChanged(nameof(Drivers));
        }
    }

    private async void OnEditDriverClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var driver = button?.CommandParameter as DriverData;
        if (driver != null)
        {
            await Navigation.PushAsync(new EditDriverDataPage(_unitOfWork, driver));
        }
    }

    public new event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}