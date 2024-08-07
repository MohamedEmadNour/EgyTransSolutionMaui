using EgyTrans.OwenSystem.DBContext.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.SystemOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepositories<T> Repositories<T>() where T : class, IIdentifiable;
        Task<DateTime> GetLastSyncTimeAsync<T>() where T : class, IIdentifiable;
        Task UpdateLastSyncTimeAsync<T>() where T : class, IIdentifiable;
        Task<int> CompleteAsync();
    }
}
