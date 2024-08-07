using EgyTrans.OwenSystem.DBContext.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.SystemOfWork.Interface
{
    public interface IApiClient
    {
        Task PushChangeAsync<T>(T entity) where T : class, IIdentifiable;
        Task<List<T>> GetNewDataAsync<T>() where T : class, IIdentifiable;
    }
}
