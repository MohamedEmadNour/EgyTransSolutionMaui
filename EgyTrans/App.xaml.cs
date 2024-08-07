
using EgyTrans.OwenSystem.DBContext;
using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.Services;
using EgyTrans.OwenSystem.SystemOfWork.Repositories;

namespace EgyTrans
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();

                MainPage = new AppShell();


            }
            catch (Exception ex)
            {
                // Log the exception or display it
                Console.WriteLine($"Error during app initialization: {ex}");
            }



        }

    }
}
