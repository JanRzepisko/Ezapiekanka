using System.ComponentModel.DataAnnotations.Schema;
using ezapiekanka.DataAccess;

namespace ezapiekanka.Models;

public sealed class Order : Entity
{
    [ForeignKey("User")] public Guid User { get; set; }
    [ForeignKey("School")] public Guid School { get; set; }
}