using Library.Repositories.Database.Entities;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;
using System.Security.Cryptography;

namespace Library.Services.Implementations;

public class AuthorizationTokenService : IAuthorizationTokenService
{
    private readonly IAuthorizationTokenRepository _authorizationTokenRepository;

    public AuthorizationTokenService(IAuthorizationTokenRepository authorizationTokenRepository)
    {
        _authorizationTokenRepository = authorizationTokenRepository;
    }

    public async Task<AuthorizationToken> GetTokenByUserIdAsync(int userId)
    {
        return await _authorizationTokenRepository.GetByUserIdAsync(userId);
    }

    private string GenerateSecureToken()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var byteArray = new byte[32];
            rng.GetBytes(byteArray);
            return Convert.ToBase64String(byteArray);
        }
    }
}