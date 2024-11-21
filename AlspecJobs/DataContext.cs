
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

            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    Id = "job-1",
                    Title = "Job 1 Title",
                    Description = "Description for Job 1",
                },
                new Job
                {
                    Id = "job-2",
                    Title = "Job 2 Title",
                    Description = "Description for Job 2",
                }
            );

            modelBuilder.Entity<SubItem>().HasData(
                new SubItem
                {
                    ItemId = "item-1",
                    JobId = "job-1",
                    Title = "SubItem 1",
                    Description = "In progress sub item",
                    Status = Status.InProgress
                },
                new SubItem
                {
                    ItemId = "item-2",
                    JobId = "job-1",
                    Title = "SubItem 2",
                    Description = "Pending sub item",
                    Status = Status.Pending
                },
                new SubItem
                {
                    ItemId = "item-3",
                    JobId = "job-2",
                    Title = "SubItem 3",
                    Description = "Completed SubItem",
                    Status = Status.Completed
                }
            );

        }
    }
}