using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EgyTrans.OwenSystem.Services
{
    public class TravelDataManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public TravelDataManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TravelData> GetTravelDataWithRelationsAsync(int travelID)
        {
            return await _unitOfWork.Repositories<TravelData>().GetByIdAsync(
                travelID,
                include :
                s =>s.Include(t => t.Cars)
                     .Include(t => t.Drivers)
                     .Include(t => t.Suppliers)
                     .Include(t => t.Client)
                     .Include(t => t.Type)
                     .Include(t => t.TravelInfos));
        }

        public async Task SaveTravelDataAsync(TravelData travel)
        {
            if (travel.TravelID == 0)
            {
                await _unitOfWork.Repositories<TravelData>().AddAsync(travel);
            }
            else
            {
                _unitOfWork.Repositories<TravelData>().Update(travel);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<TravelData>> GetAllTravelDataAsync()
        {
            var result = await _unitOfWork.Repositories<TravelData>().GetAllAsync(
                include:
                s => s.Include(t => t.Cars)
                     .Include(t => t.Drivers)
                     .Include(t => t.Suppliers)
                     .Include(t => t.Client)
                     .Include(t => t.Type)
                     .Include(t => t.TravelInfos));
            return result.ToList();
        }

        public async Task DeleteTravelDataAsync(int travelID)
        {
            var travel = await _unitOfWork.Repositories<TravelData>().GetByIdAsync(travelID);
            if (travel != null)
            {
                _unitOfWork.Repositories<TravelData>().Delete(travel);
                await _unitOfWork.CompleteAsync();
            }
        }
        public async Task<ClientData> GetClientByIdAsync(int clientId)
        {
            return await _unitOfWork.Repositories<ClientData>().GetByIdAsync(clientId);
        }

        public async Task<ClientTypeVisit> GetClientTypeVisitByIdAsync(int TypeId)
        {
            return await _unitOfWork.Repositories<ClientTypeVisit>().GetByIdAsync(TypeId);
        }


        public async Task<List<ClientData>> GetAllClientsAsync()
        {
            var result = await _unitOfWork.Repositories<ClientData>().GetAllAsync();
            return result.ToList();
        }
        public async Task<List<TourGuideClass>> GetAllselectedallTourGuidesAsync()
        {
            var result = await _unitOfWork.Repositories<TourGuideClass>().GetAllAsync();
            return result.ToList();
        }

        public async Task<List<CarData>> GetAllCarsAsync()
        {
            var result = await _unitOfWork.Repositories<CarData>().GetAllAsync();
            return result.ToList();
        }

        public async Task<List<DriverData>> GetAllDriversAsync()
        {
            var result = await _unitOfWork.Repositories<DriverData>().GetAllAsync();
            return result.ToList();
        }

        public async Task<List<SupplierData>> GetAllSuppliersAsync()
        {
            var result = await _unitOfWork.Repositories<SupplierData>().GetAllAsync();
            return result.ToList();
        }

        public async Task<List<ClientTypeVisit>> GetAllClientTypeVisitsAsync()
        {
            var result = await _unitOfWork.Repositories<ClientTypeVisit>().GetAllAsync();
            return result.ToList();
        } 
        public async Task UpdateTravelDataAsync(TravelData travelData)
        {
             _unitOfWork.Repositories<TravelData>().Update(travelData);
             await _unitOfWork.CompleteAsync();
        }
    }
}
