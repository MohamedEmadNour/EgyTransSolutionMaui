
using EgyTransApi.Services.InterFace;
using EgyTransDb.OwenSystem.DBContext;
using System.Collections;

namespace EgyTransApi.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepositories<T> Repositories<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var entityName = typeof(T).Name;

            if (!_repositories.ContainsKey(entityName))
            {
                var repositoryType = typeof(GenericRepositories<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(entityName, repositoryInstance);
            }
            return (IGenericRepositories<T>)_repositories[entityName];
        }

    }
}
