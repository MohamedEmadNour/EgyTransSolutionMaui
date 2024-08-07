using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.CRUDs.Clients
{
    public partial class AddClientPage : ContentPage, INotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;


        private ClientData _clientData;
        public ClientData ClientData
        {
            get => _clientData;
            set
            {
                _clientData = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ClientData> _clients;
        public ObservableCollection<ClientData> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        public AddClientPage(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _clientData = new ClientData();
            Clients = new ObservableCollection<ClientData>();
            BindingContext = this;
            LoadClients();
        }

        private async void LoadClients()
        {
            var clients = await _unitOfWork.Repositories<ClientData>().GetAllAsync();
            Clients.Clear();
            foreach (var client in clients)
            {
                Clients.Add(client);
            }
        }

        private async void AddClientButton_Clicked(object sender, EventArgs e)
        {
            await _unitOfWork.Repositories<ClientData>().AddAsync(_clientData);
            await _unitOfWork.CompleteAsync();
            LoadClients();

            await DisplayAlert("Success", $"Client {_clientData.ClientName} Data has been added successfully", "OK");
            ClientData = new ClientData();
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected override void OnDisappearing()
        {
            Navigation.PopAsync();
            base.OnDisappearing();
        }

        private async void OnEditClientClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var Client = button?.CommandParameter as ClientData;
            if (Client != null)
            {
                await Navigation.PushAsync(new EditClientPage(Client, _unitOfWork));
            }
        }
    }
}
