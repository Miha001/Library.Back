using Library.Repositories.Database.Entities;

namespace Library.Repositories.Interfaces;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<List<Comment>> GetCommentsByBookIdAsync(int bookId);
}