using System.ComponentModel.DataAnnotations;
namespace Trueka.Services.DTOs;

public class OfferFormDto
{
    public int ItemId { get; set; }
    [Display(Name = "Mensaje")]
    [Required(ErrorMessage = "El mensaje es obligatorio")]
    [MaxLength(500, ErrorMessage = "El mensaje no puede superar los 500 caracteres")]
    public string Message { get; set; } = null!;
}
