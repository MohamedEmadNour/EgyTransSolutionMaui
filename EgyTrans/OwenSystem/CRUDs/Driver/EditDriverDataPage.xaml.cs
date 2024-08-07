using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using Microsoft.Maui.Controls;


namespace EgyTrans.OwenSystem.CRUDs.Driver
{

    public partial class EditDriverDataPage : ContentPage
    {
        private DriverData _driver;
        private readonly IUnitOfWork _unitOfWork;

        public EditDriverDataPage(IUnitOfWork unitOfWork, DriverData driverToEdit)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _driver = driverToEdit;
            LoadDriverData();
        }

        private void LoadDriverData()
        {
            DriverNameEntry.Text = _driver.DriverName;
            DriverPhoneEntry.Text = _driver.DriverPhone.ToString();
            DriverPriceEntry.Text = _driver.DriverPrice.ToString();
            DriverTipsEntry.Text = _driver.DriverTeps.ToString();
        }

        private async void OnSaveChangesClicked(object sender, EventArgs e)
        {
            if (int.TryParse(DriverPhoneEntry.Text, out int driverPhone) &&
                int.TryParse(DriverPriceEntry.Text, out int driverPrice) &&
                int.TryParse(DriverTipsEntry.Text, out int driverTips))
            {
                _driver.DriverName = DriverNameEntry.Text;
                _driver.DriverPhone = driverPhone;
                _driver.DriverPrice = driverPrice;
                _driver.DriverTeps = driverTips;

                // Update the driver in the database
                _unitOfWork.Repositories<DriverData>().Update(_driver);
                await _unitOfWork.CompleteAsync();
                await DisplayAlert("Success", $"Driver {_driver.DriverName} has been updated successfully", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Please enter valid numeric values for Phone, Price, and Tips.", "OK");
            }
        }
    }
}