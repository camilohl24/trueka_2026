using Trueka.Entities;

namespace Trueka.Repositories.Interfaces;

public interface IOfferRepository : IGenericRepository<Offer>
{
    Task<bool> ExistsPendingAsync(int itemId, string offeredById);

    Task<IEnumerable<Offer>> GetByItemAsync(int itemId);

    Task<IEnumerable<Offer>> GetByUserAsync(string offeredById);
}