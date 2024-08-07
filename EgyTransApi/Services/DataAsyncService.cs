
using EgyTransApi.Services.InterFace;

namespace EgyTransApi.Services
{
    public class DataAsyncService : IDataAsyncService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DataAsyncService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<T>> GetAllDataAsync<T>() where T : class
        {
            return await _unitOfWork.Repositories<T>().GetAllAsync();
        }

        public async Task SetDataAsync<T>(T entity) where T : class
        {
            await _unitOfWork.Repositories<T>().AddAsync(entity);
            await _unitOfWork.CompleteAsync();
        }
    }
}
