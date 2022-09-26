using ezapiekanka.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ezapiekanka.Controllers;

[Route("/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    
    public UserController(IUserService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost("/register")]
    public async Task<IActionResult> Register(string name, string surname, Guid school, string @class, string password, string email) => await _service.Register(name, surname, school, @class, password, email);
    
    [AllowAnonymous]
    [HttpPost("/login")]
    public async Task<IActionResult> Login(string password, string email) => await _service.Login(password, email);
}