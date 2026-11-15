using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trueka.Services.DTOs;
using Trueka.Services.Interfaces;

namespace Trueka.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        => Ok(await _categoryService.GetAllAsync());

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CategoryFormDto>> GetForEdit(int id)
    {
        var category = await _categoryService.GetForEditAsync(id);
        return category is null ? NotFound() : Ok(category);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CategoryFormDto dto)
    {
        dto.Id = 0;
        await _categoryService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, CategoryFormDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new { message = "El Id de la ruta no coincide con el del cuerpo." });

        await _categoryService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deactivate(int id)
    {
        await _categoryService.DeactivateAsync(id);
        return NoContent();
    }
}