
namespace AlspecBackend.Repository.JobDao
{
    using Microsoft.EntityFrameworkCore;
    using AlspecBackend.Entities;

    public class JobRepository : IRepository<Job>
    {
        private readonly DataContext _context;
        private readonly DbSet<Job> _dbSet;

        public JobRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<Job>();
        }

        public async Task<Job?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }


        public async Task AddAsync(Job entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}