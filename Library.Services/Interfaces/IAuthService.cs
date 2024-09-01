using Library.Repositories.Database.Entities;
using LibraryService.Models;
namespace LibraryService.Services;

public interface IAuthService
{
    Task<string> LoginAsync(LoginRequest request);
    Task<bool> RegisterAsync(RegisterRequest request);
}