using ezapiekanka.DataAccess;
using ezapiekanka.Models;
using ezapiekanka.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ezapiekanka.Repository.Services;

internal class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, new()
{
    private readonly DbSet<TEntity> _entities;

    public BaseRepository(DbSet<TEntity> entities)
    {
        _entities = entities;
    }

    public ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return _entities.AddAsync(entity, cancellationToken);
    }
    
    public void Update(TEntity entity)
    {
        _entities.Update(entity);
    }

    public ValueTask<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _entities.FindAsync(new object[] { id }, cancellationToken);
    }

    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _entities.ToListAsync(cancellationToken);
    }

    public void RemoveById(Guid id)
    {
        var attachedEntity = _entities.Attach(new TEntity { Id = id });
        attachedEntity.State = EntityState.Deleted;    
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _entities.AnyAsync(c => c.Id == id, cancellationToken);
    }
}