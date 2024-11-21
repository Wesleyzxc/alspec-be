
namespace AlspecBackend.DTO
{

    public class JobDTO
    {
        public required string Id { get; set; }

        public required string Title { get; set; }
        public string? Description { get; set; }

        public IList<SubItemDTO> SubItems { get; set; } = [];
    }
}