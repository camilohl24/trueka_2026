namespace Trueka.Services.DTOs;

public class ProfileDto
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string City { get; set; } = null!;
}
