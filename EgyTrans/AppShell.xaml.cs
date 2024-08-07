using EgyTrans.OwenSystem.CRUDs.Cars;
using EgyTrans.OwenSystem.CRUDs.Clients;
using EgyTrans.OwenSystem.CRUDs.Driver;
using EgyTrans.OwenSystem.CRUDs.Supplier;
using EgyTrans.OwenSystem.CRUDs.TourGuide;
using EgyTrans.OwenSystem.CRUDs.Travel;
using EgyTrans.OwenSystem.CRUDs.TypeVisit;
using EgyTrans.OwenSystem.Pages;
using Microsoft.Maui.Controls;

namespace EgyTrans
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(ClientsPage), typeof(ClientsPage));
            Routing.RegisterRoute(nameof(ReportsPage), typeof(ReportsPage));
            Routing.RegisterRoute(nameof(AddClientPage), typeof(AddClientPage));
            Routing.RegisterRoute(nameof(EditClientPage), typeof(EditClientPage));
            Routing.RegisterRoute(nameof(AddSupplierPage), typeof(AddSupplierPage));
            Routing.RegisterRoute(nameof(AddTypeVisitPage), typeof(AddTypeVisitPage));
            Routing.RegisterRoute(nameof(AddCarDataPage), typeof(AddCarDataPage));
            Routing.RegisterRoute(nameof(AddDriverDataPage), typeof(AddDriverDataPage));
            Routing.RegisterRoute(nameof(AddTravelDataPage), typeof(AddTravelDataPage));
            Routing.RegisterRoute(nameof(addTourGuidePage), typeof(addTourGuidePage));
        }
    }
}
