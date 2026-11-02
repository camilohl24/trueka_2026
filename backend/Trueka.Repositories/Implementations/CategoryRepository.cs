using Microsoft.EntityFrameworkCore;
using Trueka.Entities;
using Trueka.Repositories.Data;
using Trueka.Repositories.Interfaces;

namespace Trueka.Repositories.Implementations;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(TruekaDbContext context) : base(context)
    {
    }
    public async Task<bool> ExistByNameAsync(string name)
        => await _dbSet.AnyAsync(c => c.Name == name);
}
