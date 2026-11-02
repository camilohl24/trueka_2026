using Microsoft.EntityFrameworkCore;
using Trueka.Entities.Common;
using Trueka.Repositories.Data;
using Trueka.Repositories.Interfaces;

namespace Trueka.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : AuditableEntity
{
    protected readonly TruekaDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(TruekaDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(bool includeInactive = false)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        if (!includeInactive)
            query = query.Where(e => e.IsActive);

        return await query.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeactivateAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return;

        entity.IsActive = false;
        await _context.SaveChangesAsync();
    }
}
