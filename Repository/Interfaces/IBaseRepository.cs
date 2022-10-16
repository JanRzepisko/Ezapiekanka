using ezapiekanka.DataAccess;
using ezapiekanka.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ezapiekanka.Repository.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : Entity, new()
{
    ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    ValueTask<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    void RemoveById(Guid id);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}