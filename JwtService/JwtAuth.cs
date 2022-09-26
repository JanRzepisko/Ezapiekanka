using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ezapiekanka.JwtService;

public class JwtAuth : IJwtAuth
{
    private readonly IConfiguration _configuration;
    
    public JwtAuth(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public Task<string> GenerateJwt(Guid id, Guid school, string? role)
    {
        byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", id.ToString()),
                new Claim("school", school.ToString()),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddDays(30),
            Audience = _configuration["Jwt:Audience"]!,
            Issuer = _configuration["Jwt:Issuer"]!,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult(tokenHandler.WriteToken(token));
    }
}