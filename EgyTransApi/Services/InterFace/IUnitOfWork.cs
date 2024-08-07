namespace EgyTransApi.Services.InterFace
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepositories<T> Repositories<T>() where T : class;
        Task<int> CompleteAsync();
    }
}
