using System.ComponentModel.DataAnnotations;

namespace Trueka.Services.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "El correo electrónico es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Password { get; set; } = null!;
}
