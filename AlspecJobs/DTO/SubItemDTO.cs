using System.Runtime.Serialization;

namespace AlspecBackend.DTO
{


    public class SubItemDTO
    {
        public required string ItemId { get; set; }
        public required string JobId { get; set; }

        public required string Title { get; set; }
        public string? Description { get; set; }

        public required string Status { get; set; }
    }
}

