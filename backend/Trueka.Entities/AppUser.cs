using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Trueka.Entities;

public class AppUser : IdentityUser
{
    [MaxLength(100)]
    public string FullName { get; set; } = null!;
    public ICollection<Item> Items { get; set; } = new List<Item>();
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();

}