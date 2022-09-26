using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ezapiekanka.Models;

public class ProductModel
{
    [Key]public Guid Id { get; set; }
    [ForeignKey("School")]public Guid School { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}