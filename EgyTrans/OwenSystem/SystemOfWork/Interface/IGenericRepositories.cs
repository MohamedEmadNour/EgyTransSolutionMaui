
using System.Linq.Expressions;
using EgyTrans.OwenSystem.DBContext.Entites;
using Microsoft.EntityFrameworkCore.Query;

namespace EgyTrans.OwenSystem.SystemOfWork.Interface
{
    public interface IGenericRepositories<T> where T : class, IIdentifiable
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<IReadOnlyList<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<List<T>> SyncGetChangedItemsAsync(DateTime lastSyncTime);
        Task SyncSaveOrUpdateAsync(T entity);
        Task<List<T>> SyncGetAllAsync();

    }
}
