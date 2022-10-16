using ezapiekanka.Models;
using ezapiekanka.Repository.Interfaces;

namespace ezapiekanka.DataAccess;

public interface IUnitOfWork
{
    IBaseRepository<User> Users { get; }
    IBaseRepository<Order> Orders { get; }
    IBaseRepository<Product> Products { get; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}