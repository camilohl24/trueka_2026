using System.ComponentModel.DataAnnotations;
namespace Trueka.Services.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "El correo electrónico es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Password { get; set; } = null!;
}
