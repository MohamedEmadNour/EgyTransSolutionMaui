using EgyTrans.OwenSystem.CRUDs.Cars;
using EgyTrans.OwenSystem.CRUDs.Clients;
using EgyTrans.OwenSystem.CRUDs.Driver;
using EgyTrans.OwenSystem.CRUDs.Supplier;
using EgyTrans.OwenSystem.CRUDs.TourGuide;
using EgyTrans.OwenSystem.CRUDs.Travel;
using EgyTrans.OwenSystem.CRUDs.TypeVisit;

namespace EgyTrans.OwenSystem.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

    }

    private async void ImageButtonAddSupplier_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddSupplierPage));
    }

    private async void ImageButtonAddType_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddTypeVisitPage));
    } 
    private async void ImageButtonAddCar_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddCarDataPage));
    }
    private async void ImageButtonAddDriver_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddDriverDataPage));
    }
    private async void ImageButtonAddTravel_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddTravelDataPage));
    }
    private async void ImageButtonAddClient_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddClientPage));
    }
    private async void ImageButtonAddGuide_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(addTourGuidePage));
    }

}