using EgyTrans.OwenSystem.CRUDs.Cars;
using EgyTrans.OwenSystem.CRUDs.Clients;
using EgyTrans.OwenSystem.CRUDs.Driver;
using EgyTrans.OwenSystem.CRUDs.Supplier;
using EgyTrans.OwenSystem.CRUDs.TourGuide;
using EgyTrans.OwenSystem.CRUDs.Travel;
using EgyTrans.OwenSystem.CRUDs.TypeVisit;
using EgyTrans.OwenSystem.DBContext;
using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.Pages;
using EgyTrans.OwenSystem.Services;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using EgyTrans.OwenSystem.SystemOfWork.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace EgyTrans
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Filename={GetDatabasePath()}"));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



            #if ANDROID
                        builder.Services.AddTransient<INotificationManagerService, EgyTrans.Platforms.Android.NotificationManagerService>();
            #elif IOS
                                                //builder.Services.AddTransient<INotificationManagerService, EgyTrans.Platforms.iOS.NotificationManagerService>();
            #elif MACCATALYST
                                                //builder.Services.AddTransient<INotificationManagerService, EgyTrans.Platforms.MacCatalyst.NotificationManagerService>();
            #elif WINDOWS
                        //builder.Services.AddTransient<INotificationManagerService, EgyTrans.Platforms.Windows.NotificationManagerService>();          
            #endif



            builder.Services.AddScoped(typeof(IGenericRepositories<>), typeof(GenericRepositories<>));

            // Register your pages
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<ClientsPage>();
            builder.Services.AddTransient<ReportsPage>();

            builder.Services.AddTransient<AddClientPage>();
            builder.Services.AddTransient<AddSupplierPage>();
            builder.Services.AddTransient<AddTypeVisitPage>();
            builder.Services.AddTransient<AddCarDataPage>();
            builder.Services.AddTransient<AddDriverDataPage>();
            builder.Services.AddTransient<AddTravelDataPage>();
            builder.Services.AddTransient<addTourGuidePage>();

            builder.Services.AddScoped<TravelDataManager>();


            builder.Services.AddHttpClient<ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri("https://localhost:44334/");
            });

            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddDebug();
            });
            builder.Logging.AddDebug();

            var app = builder.Build();

            CheckDatabaseAccess();

            Task.Run(async () =>
            {
                try
                {
                    using (var scope = app.Services.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppLogger>>();
                        var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();

                        dbContext.Database.EnsureCreated();
                        await SeedData.SeedDbData(dbContext, logger);

                        var progress = new Progress<int>(percent =>
                        {
                            Console.WriteLine($"Sync progress: {percent}%");
                        });

                        string baseUrl = "https://localhost:44334/"; 
                        await SeedData.SyncAllEntitiesAsync(dbContext, httpClientFactory, baseUrl, logger, progress);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during database initialization: {ex.Message}");
                    // You might want to log this error or handle it in some way
                }
            }).Wait();



            return app;
        }


        public static string GetDatabasePath()
        {
            return Path.Combine(FileSystem.AppDataDirectory, "OSMDatabase.db");
        }

        public static void CheckDatabaseAccess()
        {
            string dbPath = GetDatabasePath();
            Console.WriteLine($"Database path: {dbPath}");

            try
            {
                // Try to create a test file
                string testFile = Path.Combine(Path.GetDirectoryName(dbPath), "test.txt");
                File.WriteAllText(testFile, "Test");
                Console.WriteLine("Successfully wrote test file");

                // Read the content back
                string content = File.ReadAllText(testFile);
                Console.WriteLine($"Read content: {content}");

                // Delete the test file
                File.Delete(testFile);
                Console.WriteLine("Successfully deleted test file");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing file system: {ex.Message}");
            }
        }
    }
}
