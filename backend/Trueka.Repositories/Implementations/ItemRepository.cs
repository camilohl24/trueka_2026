using Microsoft.EntityFrameworkCore;
using Trueka.Entities;
using Trueka.Repositories.Data;
using Trueka.Repositories.Interfaces;

namespace Trueka.Repositories.Implementations;

public class ItemRepository : GenericRepository<Item>, IItemRepository
{
    public ItemRepository(TruekaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Item>> GetByOwnerAsync(string ownerId)
        => await _dbSet
            .Where(i => i.OwnerId == ownerId && i.IsActive)
            .Include(i => i.Category)
            .OrderByDescending(i => i.CreatedDate)
            .AsNoTracking()
            .ToListAsync();
    public async Task<IEnumerable<Item>> GetByCategoryAsync(int categoryId)
        => await _dbSet
            .Where(i => i.CategoryId == categoryId && i.IsActive)
            .Include(i => i.Owner)
            .OrderByDescending(i => i.CreatedDate)
            .AsNoTracking()
            .ToListAsync();


}
