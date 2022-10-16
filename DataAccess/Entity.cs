using System.ComponentModel.DataAnnotations;

namespace ezapiekanka.DataAccess;

public class Entity
{
    [Key]public Guid Id { get; set; }
}