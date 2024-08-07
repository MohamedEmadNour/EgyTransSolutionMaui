using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using Microsoft.Maui.Controls;

namespace EgyTrans.OwenSystem.CRUDs.Clients
{
    public partial class EditClientPage : ContentPage
    {
        private readonly IUnitOfWork _unitOfWork;
        private ClientData _clientData;

        public EditClientPage(ClientData clientData, IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _clientData = clientData;
            BindingContext = _clientData;
        }

        private async void OnEditClientClicked(object sender, EventArgs e)
        {
            // Check for validation
            if (string.IsNullOrWhiteSpace(_clientData.ClientName) || string.IsNullOrWhiteSpace(_clientData.Email))
            {
                await DisplayAlert("Validation Error", "Please enter both name and email.", "OK");
                return;
            }

            // Update client data
            _unitOfWork.Repositories<ClientData>().Update(_clientData);
            await _unitOfWork.CompleteAsync();
            await DisplayAlert("Success", $"Client data for {_clientData.ClientName} has been updated successfully.", "OK");

            // Navigate back
            await Navigation.PopAsync();
        }
    }
}
