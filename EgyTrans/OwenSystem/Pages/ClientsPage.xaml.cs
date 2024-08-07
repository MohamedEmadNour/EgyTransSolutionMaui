using EgyTrans.OwenSystem.CRUDs.Clients;
using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.Pages
{
    public partial class ClientsPage : ContentPage
    {
        private readonly INotificationManagerService notificationManager;
        public ClientsPage()
        {
            InitializeComponent();
            notificationManager = Application.Current?.MainPage?.Handler?.MauiContext?.Services.GetService<INotificationManagerService>();
            
        }


        public void Noti(string title , string messages , double timeMints) 
        {
            notificationManager.SendNotification(title, messages, DateTime.Now.AddMinutes(timeMints));
        }





        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var title = await DisplayPromptAsync("Notification title", "Notification title goes here");
            var messages = await DisplayPromptAsync("Notification messages", "Notification messages goes here");
            var Time = await DisplayPromptAsync("Notification Time", "Notification time Per Hour * 0.1 = 6 mints * ");
            var timeMinits = double.Parse(Time) * 60;
            Noti(title , messages, timeMinits);
        }
    }
}
