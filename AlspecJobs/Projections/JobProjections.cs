
namespace AlspecBackend.Projections
{
    using AlspecBackend.DTO;
    using AlspecBackend.Entities;

    public static class JobProjections
    {
        public static JobDTO ToDto(this Job entity) => new()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            SubItems = entity.SubItems.Select(s => s.ToDto()).ToList(),
        };

        public static Job ToEntity(this JobDTO job) => new()
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            SubItems = job.SubItems.Select(s => s.ToEntity()).ToList(),
        };
    }
}