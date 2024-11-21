
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
            _dbSet = _context.Jobs;
        }

        public async Task<Job[]> GetAll()
        {
            var jobs = await _dbSet.Include(j => j.SubItems).ToListAsync();
            return [.. jobs];
        }


        public async Task AddAsync(Job entity)
        {
            _dbSet.Add(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}