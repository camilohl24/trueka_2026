using Trueka.Entities;
namespace Trueka.Repositories.Interfaces;

public interface IItemRepository : IGenericRepository<Item>
{
    Task<IEnumerable<Item>> GetByOwnerAsync(string ownerId);
    Task<IEnumerable<Item>> GetByCategoryAsync(int categoryId);
}
