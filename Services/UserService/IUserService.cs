using Microsoft.AspNetCore.Mvc;

namespace ezapiekanka.Services.UserService;

public interface IUserService
{
    public Task<IActionResult> Register(string name, string surname, Guid school, string @class, string password, string email);
    public Task<IActionResult> Login(string password, string email);
}