using Microsoft.EntityFrameworkCore;
using Trueka.Entities;
using Trueka.Entities.Enums;
using Trueka.Repositories.Data;
using Trueka.Repositories.Interfaces;

namespace Trueka.Repositories.Implementations;

public class OfferRepository : GenericRepository<Offer>, IOfferRepository
{
    public OfferRepository(TruekaDbContext context) : base(context) { }

    public async Task<bool> ExistsPendingAsync(int itemId, string offeredById)
        => await _dbSet.AnyAsync(o =>
               o.ItemId == itemId
               && o.OfferedById == offeredById
               && o.Status == OfferStatus.Pending
               && o.IsActive);

    public async Task<IEnumerable<Offer>> GetByItemAsync(int itemId)
        => await _dbSet
            .Where(o => o.ItemId == itemId && o.IsActive)
            .Include(o => o.OfferedBy)
            .OrderByDescending(o => o.CreatedDate)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<Offer>> GetByUserAsync(string offeredById)
        => await _dbSet
            .Where(o => o.OfferedById == offeredById && o.IsActive)
            .Include(o => o.Item)
            .OrderByDescending(o => o.CreatedDate)
            .AsNoTracking()
            .ToListAsync();
}