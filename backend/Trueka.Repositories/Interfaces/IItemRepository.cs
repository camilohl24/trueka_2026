using Trueka.Entities;
namespace Trueka.Repositories.Interfaces;

public interface IItemRepository : IGenericRepository<Item>
{
    Task<IEnumerable<Item>> GetAllWithDetailsAsync();
    Task<Item?> GetByIdWithDetailsAsync(int id);
    Task<IEnumerable<Item>> GetByOwnerAsync(string ownerId);
    Task<IEnumerable<Item>> GetByCategoryAsync(int categoryId);
}