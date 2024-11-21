namespace AlspecBackend.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    public enum Status
    {
        Pending = 0,
        InProgress = 1,
        Completed = 2,
    }
    public class SubItem
    {
        [Key]
        public required string ItemId { get; set; }

        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }

        [ForeignKey("Job")]
        public required string JobId { get; set; }
        public Job? Job { get; set; }
    }
}

