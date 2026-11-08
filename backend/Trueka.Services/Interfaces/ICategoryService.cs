
using Trueka.Services.DTOs;

namespace Trueka.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryFormDto?> GetForEditAsync(int id);
    Task CreateAsync(CategoryFormDto dto);
    Task UpdateAsync(CategoryFormDto dto);
    Task DeactivateAsync(int id);

}
