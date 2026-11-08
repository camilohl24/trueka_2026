using Trueka.Entities.Enums;
namespace Trueka.Services.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public ItemCondition Condition { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string OwnerId { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
