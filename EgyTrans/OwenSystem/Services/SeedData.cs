using EgyTrans.OwenSystem.DBContext;
using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using EgyTrans.OwenSystem.SystemOfWork.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.Services
{
    public static class SeedData
    {
        public async static Task SeedDbData(AppDbContext context, ILogger logger)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                IUnitOfWork unitOfWork = new UnitOfWork(context);
                if (context.ClientDatas.Any() && context.CarDatas.Any() && context.ClientTypes.Any() && context.DriverDatas.Any() && context.SupplierDatas.Any() && context.TourGuides.Any())
                {
                    logger.LogInformation("Database already seeded. Skipping seeding process.");
                    return;
                }

                logger.LogInformation("Starting database seeding...");

                var clients = new ClientData { ClientName = "John Doe", Email = "john@example.com", PhoneNumber = "1234567890", Country = "Egy", PersonsCount = 5, TypeOfContact = "Mail" };
                var cars = new CarData { CarPrand = "Toyota", CarModel = "Camry", CarNum = "ABC123", CarPrincebyDay = 50, CarPrincebyHour = 10, CarDescription = "Comfortable sedan" };
                var Type = new ClientTypeVisit { TypeName = "Rental" };
                var drivers = new DriverData { DriverName = "Alice", DriverPhone = 5551234, DriverPrice = 100, DriverTeps = 0 };
                var suppliers = new SupplierData { SupplierName = "Supplier A", SupplierPhone = 5550001 };
                var tourGuides = new TourGuideClass { GuideName = "Charles", Price = 150, Telphne = 5558888, Language = "English" };

                await unitOfWork.Repositories<ClientData>().AddAsync(clients);
                await unitOfWork.Repositories<CarData>().AddAsync(cars);
                await unitOfWork.Repositories<ClientTypeVisit>().AddAsync(Type);
                await unitOfWork.Repositories<DriverData>().AddAsync(drivers);
                await unitOfWork.Repositories<SupplierData>().AddAsync(suppliers);
                await unitOfWork.Repositories<TourGuideClass>().AddAsync(tourGuides);

                await unitOfWork.CompleteAsync();
                await transaction.CommitAsync();

                logger.LogInformation("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async static Task SyncAllEntitiesAsync(AppDbContext context, IHttpClientFactory httpClientFactory, string baseUrl, ILogger logger, IProgress<int> progress = null)
        {
            try
            {
                logger.LogInformation("Starting entity synchronization...");

                var httpClient = httpClientFactory.CreateClient();
                var apiClient = new ApiClient(httpClient, baseUrl);
                var syncService = new SyncService(new UnitOfWork(context), apiClient);

                var entitiesToSync = new[]
                {
                    typeof(TourGuideClass),
                    typeof(TravelInfo),
                    typeof(TravelData),
                    typeof(SupplierData),
                    typeof(DriverData),
                    typeof(ClientTypeVisit),
                    typeof(ClientData),
                    typeof(CarData)
                };

                for (int i = 0; i < entitiesToSync.Length; i++)
                {
                    var entityType = entitiesToSync[i];
                    logger.LogInformation($"Syncing {entityType.Name}...");

                    await (Task)typeof(SyncService).GetMethod("SyncAsync")
                        .MakeGenericMethod(entityType)
                        .Invoke(syncService, null);

                    progress?.Report((i + 1) * 100 / entitiesToSync.Length);
                }

                logger.LogInformation("Entity synchronization completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during entity synchronization.");
                throw;
            }
        }
    }
}