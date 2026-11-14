using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Trueka.Services.DTOs;
using Trueka.Services.Interfaces;

namespace Trueka.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }
    private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    [HttpGet("id")]
    public async Task<ActionResult<ProfileDto>> Get(string id)
    {
        var profile = await _profileService.GetAsync(id);
        return profile is null ? NotFound() : Ok(profile);
    }

    [HttpGet("me")]
    public async Task<ActionResult<ProfileDto>> GetMine()
    {
        var form = await _profileService.GetForEditAsync(CurrentUserId);
        return form is null ? NotFound() : Ok(form);
    }
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMine(ProfileFormDto dto)
    {
        try
        {
            await _profileService.UpdateAsync(CurrentUserId, dto);
            return NoContent();

        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
