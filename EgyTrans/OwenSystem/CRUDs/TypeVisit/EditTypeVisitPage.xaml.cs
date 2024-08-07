using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;

namespace EgyTrans.OwenSystem.CRUDs.TypeVisit
{
    public partial class EditTypeVisitPage : ContentPage, INotifyPropertyChanged
    {
        private ClientTypeVisit _clientTypeVisit;
        private readonly IUnitOfWork _unitOfWork;

        public ClientTypeVisit ClientTypeVisit
        {
            get => _clientTypeVisit;
            set
            {
                _clientTypeVisit = value;
                OnPropertyChanged();
            }
        }

        public EditTypeVisitPage(ClientTypeVisit clientTypeVisit, IUnitOfWork unitOfWork)
        {
            _clientTypeVisit = clientTypeVisit;
            _unitOfWork = unitOfWork;
            BindingContext = this;
            InitializeComponent();
        }

        private async void SaveChangesButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                _unitOfWork.Repositories<ClientTypeVisit>().Update(_clientTypeVisit);
                await _unitOfWork.CompleteAsync();
                await DisplayAlert("Success", "Type Visit updated successfully", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool confirmed = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this Type Visit?", "Yes", "No");
            if (confirmed)
            {
                try
                {
                    _unitOfWork.Repositories<ClientTypeVisit>().Delete(_clientTypeVisit);
                    await _unitOfWork.CompleteAsync();
                    await DisplayAlert("Success", "Type Visit deleted successfully", "OK");
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
