using ezapiekanka.Models;
using Microsoft.EntityFrameworkCore;

namespace ezapiekanka.Services.UserService;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; } = null!;
}