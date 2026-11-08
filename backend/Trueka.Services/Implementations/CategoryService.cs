using AutoMapper;
using Trueka.Entities;
using Trueka.Repositories.Interfaces;
using Trueka.Services.DTOs;
using Trueka.Services.Interfaces;

namespace Trueka.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        => _mapper.Map<IEnumerable<CategoryDto>>(await _categoryRepository.GetAllAsync());

    public async Task<CategoryFormDto?> GetForEditAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category is null ? null : _mapper.Map<CategoryFormDto>(category);
    }

    public async Task CreateAsync(CategoryFormDto dto)
    {
        if (await _categoryRepository.ExistByNameAsync(dto.Name))
            throw new InvalidOperationException("Ya existe una categoría con ese nombre.");

        await _categoryRepository.AddAsync(_mapper.Map<Category>(dto));
    }

    public async Task UpdateAsync(CategoryFormDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(dto.Id)
            ?? throw new KeyNotFoundException($"Category {dto.Id} not found.");

        var nameChanged = !string.Equals(category.Name, dto.Name, StringComparison.OrdinalIgnoreCase);
        if (nameChanged && await _categoryRepository.ExistByNameAsync(dto.Name))
            throw new InvalidOperationException("Ya existe una categoría con ese nombre.");

        _mapper.Map(dto, category);
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeactivateAsync(int id)
        => await _categoryRepository.DeactivateAsync(id);
}