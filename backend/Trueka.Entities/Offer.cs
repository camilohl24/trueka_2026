using System.ComponentModel.DataAnnotations;
using Trueka.Entities.Common;
using Trueka.Entities.Enums;

namespace Trueka.Entities;

public class Offer : AuditableEntity
{
    [MaxLength(500)]
    public string Message { get; set; } = null!;
    public OfferStatus Status { get; set; } = OfferStatus.Pending;
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    public string OfferedById { get; set; } = null!;
    public AppUser OfferedBy { get; set; } = null!;

}
