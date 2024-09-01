using Library.Core.Models;
using Library.Repositories.Database.Entities;

namespace Library.Services.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetByBookIdAsync(int bookId);
    Task AddAsync(AddCommentRequest comment);
    Task DeleteAsync(int id);
}