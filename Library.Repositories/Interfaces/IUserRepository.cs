using Library.Repositories.Database.Entities;

namespace Library.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
}