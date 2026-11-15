namespace Trueka.Services.DTOs;

public class AuthResponseDto
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public string UserId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public IList<string> Roles { get; set; } = new List<string>();
}
