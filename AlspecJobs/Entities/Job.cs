
namespace AlspecBackend.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Job
    {
        [Key]
        public required string Id { get; set; }

        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }

        public IList<SubItem> SubItems { get; set; } = [];
    }
}