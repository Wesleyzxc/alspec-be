namespace AlspecBackend.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T[]> GetAll();
        Task AddAsync(T entity);
        Task SaveAsync();
    }
}