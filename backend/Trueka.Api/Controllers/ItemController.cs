using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Trueka.Services.DTOs;
using Trueka.Services.Interfaces;

namespace Trueka.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetAll()
        => Ok(await _itemService.GetAllAsync());

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<ItemDetailDto>> GetDetail(int id)
    {
        var item = await _itemService.GetDetailAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("category/{categoryId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetByCategory(int categoryId)
        => Ok(await _itemService.GetByCategoryAsync(categoryId));

    [HttpGet("owner/{ownerId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetByOwner(string ownerId)
        => Ok(await _itemService.GetByOwnerAsync(ownerId));

    [HttpGet("mine")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetMine()
        => Ok(await _itemService.GetByOwnerAsync(CurrentUserId));

    [HttpGet("{id:int}/edit")]
    [Authorize]
    public async Task<ActionResult<ItemFormDto>> GetForEdit(int id)
    {
        var item = await _itemService.GetForEditAsync(id, CurrentUserId);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(ItemFormDto dto)
    {
        dto.Id = 0;
        await _itemService.CreateAsync(dto, CurrentUserId);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, ItemFormDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new { message = "El Id de la ruta no coincide con el del cuerpo." });

        await _itemService.UpdateAsync(dto, CurrentUserId);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Deactivate(int id)
    {
        await _itemService.DeactivateAsync(id, CurrentUserId);
        return NoContent();
    }
}