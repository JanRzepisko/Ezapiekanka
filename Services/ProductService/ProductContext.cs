using ezapiekanka.Models;
using Microsoft.EntityFrameworkCore;

namespace ezapiekanka.Services.ProductService;

public class ProductContext : DbContext
{
    DbSet<Product> Products  { get; set; } = null!;
    DbSet<Order> Orders  { get; set; } = null!;
    
    public ProductContext(DbContextOptions options) : base(options)
    {
    }
} 