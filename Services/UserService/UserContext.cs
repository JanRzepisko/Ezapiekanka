using ezapiekanka.DataAccess;
using ezapiekanka.Models;
using ezapiekanka.Repository.Interfaces;
using ezapiekanka.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace ezapiekanka.Services.UserService;

public class UserContext : DbContext, IUnitOfWork
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    private DbSet<User> DbSetUser { get; set; } = null!;
    private DbSet<Product> DbSetProduct { get; set; } = null!;
    private DbSet<Order> DbSetOrder { get; set; } = null!;

    public IBaseRepository<User> Users => new BaseRepository<User>(DbSetUser);
    public IBaseRepository<Product> Products => new BaseRepository<Product>(DbSetProduct);
    public IBaseRepository<Order> Orders => new BaseRepository<Order>(DbSetOrder);
}