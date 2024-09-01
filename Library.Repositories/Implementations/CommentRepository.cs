using Library.Repositories.Database;
using Library.Repositories.Database.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Implementations;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository, IBaseRepository<Comment>
{
    public CommentRepository(LibraryContext context) : base(context) { }

    public async Task<List<Comment>> GetCommentsByBookIdAsync(int bookId)
    {
        return await _context.Comments.Where(c => c.BookId == bookId && !c.IsDeleted).ToListAsync();
    }
}