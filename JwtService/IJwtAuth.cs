namespace ezapiekanka.JwtService;

public interface IJwtAuth
{
    public Task<string> GenerateJwt(Guid id, Guid school, string? role);
}