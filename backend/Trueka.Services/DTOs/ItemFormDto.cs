using System.ComponentModel.DataAnnotations;
using Trueka.Entities.Enums;
namespace Trueka.Services.DTOs;

public class ItemFormDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Description is required.")]
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; } = null!;

    [EnumDataType(typeof(ItemCondition), ErrorMessage = "Invalid condition.")]
    public ItemCondition Condition { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Select a category.")]
    public int CategoryId { get; set; }
}
