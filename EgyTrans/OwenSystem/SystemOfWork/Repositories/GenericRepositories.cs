using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EgyTrans.OwenSystem.DBContext;
using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;




namespace EgyTrans.OwenSystem.SystemOfWork.Repositories
{
    public class GenericRepositories<T> : IGenericRepositories<T> where T : class, IIdentifiable 
    {
        private readonly AppDbContext _context;

        public GenericRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            var keyProperty = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.First();
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, keyProperty.Name).Equals(id));
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }


        public async Task<List<T>> SyncGetChangedItemsAsync(DateTime lastSyncTime)
        {
            return await _context.Set<T>()
                .Where(e => e.UpdatedAt > lastSyncTime || e.DeletedAt > lastSyncTime)
                .ToListAsync();
        }

        public async Task SyncSaveOrUpdateAsync(T entity)
        {
            var id = entity.GetId();
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity == null)
            {
                await _context.Set<T>().AddAsync(entity);
            }
            else
            {
                _context.Set<T>().Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> SyncGetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }




    }
}
