using Library.Repositories.Database.Entities;

namespace Library.Services.Interfaces;

public interface IAuthorizationTokenService
{
    Task<AuthorizationToken> GetTokenByUserIdAsync(int userId);
}