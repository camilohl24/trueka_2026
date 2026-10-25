using System.ComponentModel.DataAnnotations;
using Trueka.Entities.Common;
namespace Trueka.Entities;

using Trueka.Entities.Enums;

public class Item : AuditableEntity
{
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string Description { get; set; } = null!;
    public ItemCondition Condition { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public string OwnerId { get; set; } = null!;
    public AppUser Owner { get; set; } = null!;
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();

}
