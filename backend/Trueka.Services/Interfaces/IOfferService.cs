using Trueka.Services.DTOs;

namespace Trueka.Services.Interfaces;

public interface IOfferService
{
    Task MakeOfferAsync(string userId, OfferFormDto dto);

    Task<IEnumerable<OfferDto>> GetMyOffersAsync(string userId);
}