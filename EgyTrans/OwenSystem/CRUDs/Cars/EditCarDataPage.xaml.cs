using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;

namespace EgyTrans.OwenSystem.CRUDs.Cars;

public partial class EditCarDataPage : ContentPage
{
    private CarData car;
    private readonly IUnitOfWork _unitOfWork;
    public EditCarDataPage(IUnitOfWork unitOfWork, CarData carToEdit)
    {
        InitializeComponent();
        _unitOfWork = unitOfWork;
        car = carToEdit;
        LoadCarData();
    }
    private void LoadCarData()
    {
        CarPrandEntry.Text = car.CarPrand;
        CarModelEntry.Text = car.CarModel;
        CarNumEntry.Text = car.CarNum.ToString();
        CarPrinceByDayEntry.Text = car.CarPrincebyDay.ToString();
        CarPrinceByHourEntry.Text = car.CarPrincebyHour.ToString();
        CarDescriptionEditor.Text = car.CarDescription;
    }

    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        if (int.TryParse(CarPrinceByDayEntry.Text, out int priceByDay) &&
            int.TryParse(CarPrinceByHourEntry.Text, out int priceByHour)
            )
        {

            car.CarPrand = CarPrandEntry.Text;
            car.CarModel = CarModelEntry.Text;
            car.CarNum = CarNumEntry.Text;
            car.CarPrincebyDay = priceByDay;
            car.CarPrincebyHour = priceByHour;
            car.CarDescription = CarDescriptionEditor.Text;

            // TODO: Update the car in your database
            _unitOfWork.Repositories<CarData>().Update(car);
            await _unitOfWork.CompleteAsync();
            await DisplayAlert("Success", $"Car {car.CarPrand} {car.CarModel} has been Updated successfully", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Please enter valid numeric values for Car Number, Price per Day, and Price per Hour.", "OK");
        }
    }
}