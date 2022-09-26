using ezapiekanka.Models;
using Microsoft.EntityFrameworkCore;

namespace ezapiekanka.Services.ProductService;

public class ProductContext : DbContext
{
    DbSet<ProductModel> Products  { get; set; } = null!;

    public ProductContext(DbContextOptions options) : base(options)
    {
    }
} 