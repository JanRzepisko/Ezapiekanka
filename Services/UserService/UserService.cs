using ezapiekanka.DataAccess;
using ezapiekanka.JwtService;
using ezapiekanka.Models;
using ezapiekanka.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ezapiekanka.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtAuth _jwt;

    public UserService(IJwtAuth jwt, IUserRepository userManager, IUnitOfWork unitOfWork)
    {
        _jwt = jwt;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IActionResult> Register(string name, string surname, Guid school, string @class, string password, string email)
    {
        Guid.NewGuid();
        Guid id;
        do
        {
            id = Guid.NewGuid();
        } while (await _userManager.ExistsAsync(id));


        User user = new User {
            Email = email,
            Id = id,
            Name = name,
            Role = JwtPolicies.User,
            School = school, 
            Surname = surname,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, 4), 
            HaveAvatar = false,
            IsActive = false,
            Class = @class
        };
        
        await _userManager.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        
        //Send Email or check on school list        
        
        return new OkResult();
    }
    public async Task<IActionResult> Login(string password, string email)
    {
        User? user;
        
        if (await _userManager.EmailExistAsync(email))
        {
            user = await _userManager.GetByEmail(email);
            
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return new ConflictObjectResult("BadPassword");
            if (!user.IsActive) return  new ConflictObjectResult("UserIsNotActive");
        }
        else
        {
            return  new ConflictObjectResult("UserIsNotExist");
        }
        
        string jwt = await _jwt.GenerateJwt(user.Id, user.School, user.Role);
        object response = new {
            id=user.Id, school=user.School, name=user.Name, surname=user.Surname, email=user.Email, @class=user.Class, role=user.Role, haveAvatar=user.HaveAvatar, Jwt=jwt
        };
        return new ObjectResult(response);
    }
}