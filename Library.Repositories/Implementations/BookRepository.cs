using Library.Repositories.Database;
using Library.Repositories.Database.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Implementations;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(LibraryContext context) : base(context) { }
    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books.Where(b => !b.IsDeleted).ToListAsync();
    }
}
