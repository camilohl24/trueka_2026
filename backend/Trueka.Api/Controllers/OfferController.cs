using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Trueka.Services.DTOs;
using Trueka.Services.Interfaces;

namespace Trueka.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OfferController : ControllerBase
{
    private readonly IOfferService _offerService;

    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpPost]
    public async Task<IActionResult> MakeOffer(OfferFormDto dto)
    {
        await _offerService.MakeOfferAsync(CurrentUserId, dto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("mine")]
    public async Task<ActionResult<IEnumerable<OfferDto>>> GetMine()
        => Ok(await _offerService.GetMyOffersAsync(CurrentUserId));
}