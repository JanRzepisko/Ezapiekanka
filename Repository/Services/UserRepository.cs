using ezapiekanka.DataAccess;
using ezapiekanka.Models;
using ezapiekanka.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ezapiekanka.Repository.Services;

public class UserRepository : IUserRepository
{
    private readonly IUserRepository _userRepositoryImplementation;
    private readonly DbSet<User> _entities;

    public UserRepository(DbSet<User> entities, IUserRepository userRepositoryImplementation)
    {
        _entities = entities;
        _userRepositoryImplementation = userRepositoryImplementation;
    }
    
    public ValueTask<EntityEntry<User>> AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        return _userRepositoryImplementation.AddAsync(entity, cancellationToken);
    }
    public void Update(User entity)
    {
        _userRepositoryImplementation.Update(entity);
    }
    public ValueTask<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _userRepositoryImplementation.GetByIdAsync(id, cancellationToken);
    }
    public Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _userRepositoryImplementation.GetAllAsync(cancellationToken);
    }
    public void RemoveById(Guid id)
    {
        _userRepositoryImplementation.RemoveById(id);
    }
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _userRepositoryImplementation.ExistsAsync(id, cancellationToken);
    }
    public Task<User> GetByEmail(string email)
    {
        return _entities.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email)!;
    }
    public Task<bool> EmailExistAsync(string email)
    {
        return _entities.AnyAsync(x => x.Email == email);
    }
}