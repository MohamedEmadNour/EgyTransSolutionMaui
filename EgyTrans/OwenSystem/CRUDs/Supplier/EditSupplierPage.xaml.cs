using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;

namespace EgyTrans.OwenSystem.CRUDs.Supplier;

public partial class EditSupplierPage : ContentPage
{
	private readonly SupplierData _supplier;
	private readonly IUnitOfWork _unitOfWork;

    public EditSupplierPage(SupplierData supplier , IUnitOfWork unitOfWork )
	{
        _supplier = supplier;
        _unitOfWork	= unitOfWork;
        BindingContext = this;
		InitializeComponent();
	}
    private void LoadDriverData()
    {
        SupplierNameEntry.Text = _supplier.SupplierName;
        SupplierPhoneEntry.Text = _supplier.SupplierPhone.ToString();

    }

    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        if (int.TryParse(SupplierPhoneEntry.Text, out int driverPhone))
        {
            _supplier.SupplierName = SupplierNameEntry.Text;
            _supplier.SupplierPhone = driverPhone;


            // Update the driver in the database
            _unitOfWork.Repositories<SupplierData>().Update(_supplier);
            await _unitOfWork.CompleteAsync();
            await DisplayAlert("Success", $"Driver {_supplier.SupplierName} has been updated successfully", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Please enter valid numeric values for Phone, Price, and Tips.", "OK");
        }
    }
}