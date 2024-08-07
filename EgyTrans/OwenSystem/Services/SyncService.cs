using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.Services
{
    public class SyncService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiClient _apiClient;

        public SyncService(IUnitOfWork unitOfWork, IApiClient apiClient)
        {
            _unitOfWork = unitOfWork;
            _apiClient = apiClient;
        }

        public async Task SyncAsync<T>() where T : class, IIdentifiable, new()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return;
            }

            var repository = _unitOfWork.Repositories<T>();

            var lastSyncTime = await _unitOfWork.GetLastSyncTimeAsync<T>();
            var localChanges = await repository.SyncGetChangedItemsAsync(lastSyncTime);

            // Push local changes to server
            foreach (var change in localChanges)
            {
                await _apiClient.PushChangeAsync(change);
            }

            // Get new data from server
            var newData = await _apiClient.GetNewDataAsync<T>();
            foreach (var data in newData)
            {
                await repository.SyncSaveOrUpdateAsync(data);
            }

            await _unitOfWork.UpdateLastSyncTimeAsync<T>();
        }
    }

}
