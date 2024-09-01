using Library.Repositories.Database.Entities;

namespace Library.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task RegisterUserAsync(User user);
    }
}