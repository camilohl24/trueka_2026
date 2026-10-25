using System.ComponentModel.DataAnnotations;
using Trueka.Entities.Common;

namespace Trueka.Entities;

public class Category : AuditableEntity
{
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    public ICollection<Item> Items { get; set; } = new List<Item>();

}
