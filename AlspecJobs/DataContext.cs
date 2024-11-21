
namespace AlspecBackend
{
    using AlspecBackend.Entities;
    using Microsoft.EntityFrameworkCore;
    public class DataContext : DbContext
    {
        public DataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "alspec.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");


        public DbSet<Job> Jobs { get; set; }
        public DbSet<SubItem> SubItems { get; set; }

        public string DbPath { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasIndex(j => j.Id);

            modelBuilder.Entity<SubItem>()
                .HasOne<Job>()
                .WithMany(s => s.SubItems)
                .HasForeignKey(s => s.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubItem>()
                .Property(s => s.Status)
                .HasConversion<string>();

            modelBuilder.Entity<SubItem>()
            .Property(s => s.Status)
            .HasConversion(
                v => v.ToString(),
                v => (Status)Enum.Parse(typeof(Status), v)
            );
        }
    }
}