using Library.Core.Configuration;
using Library.Repositories.Database.Entities;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;
using LibraryService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Web;

namespace LibraryService.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthorizationTokenService _tokenService;
    private readonly IAuthorizationTokenRepository _authorizationTokenRepository;
    private readonly ITokenHasher _tokenHasher;
    private readonly AppSettings _appSettings;

    public AuthService(
        IUserRepository userRepository,
        IConfiguration configuration,
        IPasswordHasher passwordHasher,
        ITokenHasher tokenHasher,
        IAuthorizationTokenService authorizationTokenService,
        IAuthorizationTokenRepository authorizationTokenRepository,
        IOptions<AppSettings> options)
    {
        _tokenHasher = tokenHasher;
        _userRepository = userRepository;
        _configuration = configuration;
        _passwordHasher = passwordHasher;
        _tokenService = authorizationTokenService;
        _appSettings = options.Value;
        _authorizationTokenRepository = authorizationTokenRepository;
    }

    public async Task<string> LoginAsync(LoginRequest request)
    {
        if (!String.IsNullOrEmpty(request.UserName) && !String.IsNullOrEmpty(request.Password))
        {
            var user = await _userRepository.GetByUsernameAsync(request.UserName);
            if (user != null && user.PasswordHash != null)
            {
                var passwordAreEquals = _passwordHasher
                    .VerifyHashedPassword(user.PasswordHash, request.Password);
                if (passwordAreEquals)
                {
                    var userToken = await _tokenService.GetTokenByUserIdAsync(user.Id);

                    if (userToken != null)
                    {
                        _authorizationTokenRepository.Delete(userToken);
                    }

                    var guid = Guid.NewGuid();
                    var token = _tokenHasher.HashToken(request.Password, guid.ToString());
                    var newToken = new AuthorizationToken
                    {
                        UserId = user.Id,
                        Expiration = DateTime.UtcNow.AddDays(_appSettings.UsersExpirationDays),
                        Token = token
                    };
                    await _authorizationTokenRepository.AddAsync(newToken);
                    await _authorizationTokenRepository.SaveChangesAsync();
                    token = HttpUtility.UrlEncode(token);
                    return token;
                }
            }
        }
        return null;
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var requestUser = await _userRepository.GetByUsernameAsync(request.UserName);
        if (requestUser == null)
        {
            var passwordHash = _passwordHasher.HashPassword(request.Password);
            var newUser = new User
            {
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                MiddleName = request.MiddleName,
                PasswordHash = passwordHash,
                RoleId = 2, //По дефолту указываем для юзера роль id = 2, TODO: вынести в энамку
                Username = request.UserName,
                Email = request.Email
            };
                
            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();
            return true;
        }
        return false;
    }
}