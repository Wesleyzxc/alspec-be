
namespace AlspecBackend.Projections
{
    using System.ComponentModel.DataAnnotations;
    using AlspecBackend.DTO;
    using AlspecBackend.Entities;

    public static class SubItemProjections
    {
        public static SubItemDTO ToDto(this SubItem entity) => new()
        {
            ItemId = entity.ItemId,
            JobId = entity.JobId,
            Title = entity.Title,
            Description = entity.Description,
            Status = entity.Status.ToString(),
        };

        public static SubItem ToEntity(this SubItemDTO subItem)
        {
            if (!Enum.TryParse<Status>(subItem.Status, true, out var status))
            {
                throw new ValidationException("Status is not valid");
            }

            var subItemEntity = new SubItem
            {
                ItemId = subItem.ItemId,
                JobId = subItem.JobId,
                Title = subItem.Title,
                Description = subItem.Description,
                Status = status,
            };

            return subItemEntity;
        }
    }
}