namespace AlspecBackend.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task SaveAsync();
    }
}