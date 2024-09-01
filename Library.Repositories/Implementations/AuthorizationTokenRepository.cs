using Library.Repositories.Database.Entities;
using Library.Repositories.Database;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Implementations;


public class AuthorizationTokenRepository : BaseRepository<AuthorizationToken>, IAuthorizationTokenRepository
{
    public AuthorizationTokenRepository(LibraryContext context) : base(context) { }

    public async Task<AuthorizationToken> GetByTokenAsync(string token)
    {
        return await _context.AuthorizationTokens
                             .Include(at => at.User)
                             .SingleOrDefaultAsync(at => at.Token == token && !at.User.IsDeleted);
    }

    public async Task<AuthorizationToken> GetByUserIdAsync(int userId)
    {
        return await _context.AuthorizationTokens
                             .Include(at => at.User)
                             .SingleOrDefaultAsync(at => at.UserId == userId && !at.User.IsDeleted);
    }
}