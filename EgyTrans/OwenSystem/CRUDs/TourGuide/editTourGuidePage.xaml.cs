using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;

namespace EgyTrans.OwenSystem.CRUDs.TourGuide;

public partial class editTourGuidePage : ContentPage
{

    private TourGuideClass _guide;
    private readonly IUnitOfWork _unitOfWork;
    public editTourGuidePage(IUnitOfWork unitOfWork , TourGuideClass tourGuide)
	{
        _unitOfWork = unitOfWork;
        _guide = tourGuide;
		InitializeComponent();
        LoadGuideData();
    }

    private void LoadGuideData()
    {
        GuideNameEntry.Text = _guide.GuideName;
        GuideLanguageEntry.Text = _guide.Language;
        GuidePhoneEntry.Text = _guide.Telphne.ToString();
        GuidePriceEntry.Text = _guide.Price.ToString();
    }

    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        if (int.TryParse(GuidePhoneEntry.Text, out int driverPhone) &&
            int.TryParse(GuidePriceEntry.Text, out int driverPrice))
        {
            _guide.GuideName = GuideNameEntry.Text;
            _guide.Language = GuideLanguageEntry.Text;
            _guide.Telphne = driverPhone;
            _guide.Price = driverPrice;

            // Update the driver in the database
            _unitOfWork.Repositories<TourGuideClass>().Update(_guide);
            await _unitOfWork.CompleteAsync();
            await DisplayAlert("Success", $"Driver {_guide.GuideName} has been updated successfully", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Please enter valid numeric values for Phone, Price, and Tips.", "OK");
        }
    }


}