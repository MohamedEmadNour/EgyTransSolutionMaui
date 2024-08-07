namespace EgyTransApi.Services
{
    public interface IDataAsyncService
    {
        Task<IEnumerable<T>> GetAllDataAsync<T>() where T : class;
        Task SetDataAsync<T>(T entity) where T : class;
    }
}
