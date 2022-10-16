using ezapiekanka.Models;

namespace ezapiekanka.Repository.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByEmail(string email);    
    Task<bool> EmailExistAsync(string email);
}