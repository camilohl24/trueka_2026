using System.ComponentModel.DataAnnotations;
namespace Trueka.Services.DTOs
{
    public class CategoryFormDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public string Name { get; set; } = null!;
    }
}
