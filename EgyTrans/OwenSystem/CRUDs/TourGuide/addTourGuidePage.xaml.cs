
using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EgyTrans.OwenSystem.CRUDs.TourGuide;

public partial class addTourGuidePage : ContentPage, INotifyPropertyChanged
{

    private TourGuideClass _guideData;
    private readonly IUnitOfWork _unitOfWork;
    private string _searchText;


    public TourGuideClass GuideData
    {
        get => _guideData;
        set
        {
            _guideData = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<TourGuideClass> Guides { get; set; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            FilterGuides();
        }
    }
    public addTourGuidePage(IUnitOfWork unitOfWork)
    {
        InitializeComponent();
        _unitOfWork = unitOfWork;
        _guideData = new TourGuideClass();
        Guides = new ObservableCollection<TourGuideClass>();
        BindingContext = this;
        LoadData();
    }

    private async void LoadData()
    {
        var guides = await _unitOfWork.Repositories<TourGuideClass>().GetAllAsync();
        Guides = new ObservableCollection<TourGuideClass>(guides);
        OnPropertyChanged(nameof(Guides));
    }

    private async void AddGuideButton_Clicked(object sender, EventArgs e)
    {
        await _unitOfWork.Repositories<TourGuideClass>().AddAsync(_guideData);
        await _unitOfWork.CompleteAsync();
        LoadData();
        await DisplayAlert("Success", $"Driver {_guideData.GuideName} has been added successfully", "OK");

        GuideData = new TourGuideClass();
    }

    private void FilterGuides()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            LoadData();
        }
        else
        {
            var filteredGuides = Guides.Where(d =>
                d.GuideName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                d.Telphne.ToString().Contains(SearchText)).ToList();
            Guides = new ObservableCollection<TourGuideClass>(filteredGuides);
            OnPropertyChanged(nameof(Guides));
        }
    }

    private async void OnEditGuideClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var guide = button?.CommandParameter as TourGuideClass;
        if (guide != null)
        {
            await Navigation.PushAsync(new editTourGuidePage(_unitOfWork, guide));
        }
    }

    public new event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}