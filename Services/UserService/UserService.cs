using ezapiekanka.JwtService;
using ezapiekanka.Models;
using Microsoft.AspNetCore.Mvc;

namespace ezapiekanka.Services.UserService;

public class UserService : IUserService
{
    private readonly UserContext _users;
    private readonly IJwtAuth _jwt;

    public UserService(UserContext users, IJwtAuth jwt)
    {
        _users = users;
        _jwt = jwt;
    }
    
    public async Task<IActionResult> Register(string name, string surname, Guid school, string @class, string password, string email)
    {
        Guid id;
        do
        {
            id = Guid.NewGuid();

        } while (_users.Users.Any(x => x.Id == id));

        _users.Users.Add(new UserModel()
        {
            Email = email, Id = id, Name = name, Role = JwtPolicies.User, School = school, Surname = surname, PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, 4), HaveAvatar = false, IsActive = false, Class = @class
        });
        
        await _users.SaveChangesAsync();
        
        //Send Email or check on school list        
        
        return new OkResult();
    }

    public async Task<IActionResult> Login(string password, string email)
    {
        UserModel? user;
        
        if (_users.Users.Any(x => x.Email == email))
        {
            user = _users.Users.First(x => x.Email == email);
            
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return new ConflictObjectResult("BadPassword");
            if (!user.IsActive) return  new ConflictObjectResult("UserIsNotActive");
        }
        else
        {
            return  new ConflictObjectResult("UserIsNotExist");
        }
        
        string jwt = await _jwt.GenerateJwt(user.Id, user.School, user.Role);
        object response = new {
            Id=user.Id, School=user.School, Name=user.Name, Surname=user.Surname, Email=user.Email, Class=user.Class, Role=user.Role, HaveAvatar=user.HaveAvatar, Jwt=jwt
        };
        return new ObjectResult(response);
    }
}