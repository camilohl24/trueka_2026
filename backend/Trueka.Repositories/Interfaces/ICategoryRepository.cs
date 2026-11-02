using Trueka.Entities;
namespace Trueka.Repositories.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<bool> ExistByNameAsync(string name);
}
