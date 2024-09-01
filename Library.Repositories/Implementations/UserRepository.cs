using Library.Repositories.Database;
using Library.Repositories.Database.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Implementations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(LibraryContext context) : base(context) { }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Username == username && !u.IsDeleted);
    }
}