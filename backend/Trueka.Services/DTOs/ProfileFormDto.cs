using System.ComponentModel.DataAnnotations;
namespace Trueka.Services.DTOs;

public class ProfileFormDto
{
    [Required(ErrorMessage = "Full name is required")]
    [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
    public string FullName { get; set; } = null!;

    [MaxLength(500, ErrorMessage = "Image URL cannot exceed 500 characters")]
    [Url(ErrorMessage = "Invalid URL format (http:// or https://)")]
    public string? ImageUrl { get; set; }


    [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters")]
    public string? City { get; set; }

}
