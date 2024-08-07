using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace EgyTransApi.Services.InterFace
{
    public interface IGenericRepositories<T> where T : class
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
    }
}
