using Trueka.Entities.Enums;
namespace Trueka.Services.DTOs;

public class ItemDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ItemCondition Condition { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string OwnerId { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}
