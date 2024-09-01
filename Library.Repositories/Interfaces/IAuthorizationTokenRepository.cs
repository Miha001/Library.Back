using Library.Repositories.Database.Entities;

namespace Library.Repositories.Interfaces;

public interface IAuthorizationTokenRepository : IBaseRepository<AuthorizationToken>
{
    Task<AuthorizationToken> GetByTokenAsync(string token);
    Task<AuthorizationToken> GetByUserIdAsync(int userId);
}