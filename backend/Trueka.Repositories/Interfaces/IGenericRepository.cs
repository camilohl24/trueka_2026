using Trueka.Entities.Common;

namespace Trueka.Repositories.Interfaces;

public interface IGenericRepository<T> where T : AuditableEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(bool includeInactive = false);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeactivateAsync(int id);
}
