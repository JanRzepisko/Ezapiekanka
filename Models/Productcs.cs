using System.ComponentModel.DataAnnotations.Schema;
using ezapiekanka.DataAccess;

namespace ezapiekanka.Models;

public sealed class Product : Entity
{
    [ForeignKey("School")]public Guid School { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}