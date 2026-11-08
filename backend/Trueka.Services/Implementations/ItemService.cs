using AutoMapper;
using Trueka.Entities;
using Trueka.Repositories.Interfaces;
using Trueka.Services.DTOs;
using Trueka.Services.Interfaces;

namespace Trueka.Services.Implementations;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public ItemService(
        IItemRepository itemRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _itemRepository = itemRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ItemDto>> GetAllAsync()
        => _mapper.Map<IEnumerable<ItemDto>>(await _itemRepository.GetAllWithDetailsAsync());

    public async Task<ItemDetailDto?> GetDetailAsync(int id)
    {
        var item = await _itemRepository.GetByIdWithDetailsAsync(id);
        return item is null ? null : _mapper.Map<ItemDetailDto>(item);
    }

    public async Task<IEnumerable<ItemDto>> GetByOwnerAsync(string ownerId)
        => _mapper.Map<IEnumerable<ItemDto>>(await _itemRepository.GetByOwnerAsync(ownerId));

    public async Task<IEnumerable<ItemDto>> GetByCategoryAsync(int categoryId)
        => _mapper.Map<IEnumerable<ItemDto>>(await _itemRepository.GetByCategoryAsync(categoryId));

    public async Task<ItemFormDto?> GetForEditAsync(int id, string currentUserId)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item is null) return null;

        EnsureOwner(item, currentUserId);
        return _mapper.Map<ItemFormDto>(item);
    }

    public async Task CreateAsync(ItemFormDto dto, string ownerId)
    {
        await EnsureActiveCategoryAsync(dto.CategoryId);

        var item = _mapper.Map<Item>(dto);
        item.OwnerId = ownerId;

        await _itemRepository.AddAsync(item);
    }

    public async Task UpdateAsync(ItemFormDto dto, string currentUserId)
    {
        var item = await _itemRepository.GetByIdAsync(dto.Id)
            ?? throw new KeyNotFoundException($"Item {dto.Id} not found.");

        EnsureOwner(item, currentUserId);
        await EnsureActiveCategoryAsync(dto.CategoryId);

        _mapper.Map(dto, item);
        await _itemRepository.UpdateAsync(item);
    }

    public async Task DeactivateAsync(int id, string currentUserId)
    {
        var item = await _itemRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Item {id} not found.");

        EnsureOwner(item, currentUserId);
        await _itemRepository.DeactivateAsync(id);
    }

    private static void EnsureOwner(Item item, string userId)
    {
        if (item.OwnerId != userId)
            throw new UnauthorizedAccessException("Solo el dueño puede modificar este objeto.");
    }

    private async Task EnsureActiveCategoryAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null || !category.IsActive)
            throw new InvalidOperationException("La categoría seleccionada no existe o no está activa.");
    }
}