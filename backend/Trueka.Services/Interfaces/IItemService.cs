using Trueka.Services.DTOs;

namespace Trueka.Services.Interfaces;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetAllAsync();

    Task<ItemDetailDto?> GetDetailAsync(int id);

    Task<IEnumerable<ItemDto>> GetByOwnerAsync(string ownerId);

    Task<IEnumerable<ItemDto>> GetByCategoryAsync(int categoryId);

    Task<ItemFormDto?> GetForEditAsync(int id, string currentUserId);

    Task CreateAsync(ItemFormDto dto, string ownerId);

    Task UpdateAsync(ItemFormDto dto, string currentUserId);

    Task DeactivateAsync(int id, string currentUserId);
}