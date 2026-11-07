using Trueka.Entities.Enums;

namespace Trueka.Services.DTOs;

public class OfferDto
{
    public int Id { get; set; }
    public string? ItemTitle { get; set; }
    public string? OfferedByFullName { get; set; }
    public string Message { get; set; } = null!;

    public OfferStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
}
