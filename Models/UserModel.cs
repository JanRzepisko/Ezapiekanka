using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ezapiekanka.Models;

public class UserModel
{
    [Key]public Guid Id { get; set; }
    [ForeignKey("school")]public Guid School { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? Class { get; set; }
    public string? Role { get; set; }
    public bool HaveAvatar { get; set; }
    public bool IsActive { get; set; }
}