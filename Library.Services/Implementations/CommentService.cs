using Library.Core.Models;
using Library.Repositories.Database.Entities;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;

namespace Library.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<Comment>> GetByBookIdAsync(int bookId)
    {
        return await _commentRepository.GetCommentsByBookIdAsync(bookId);
    }

    public async Task AddAsync(AddCommentRequest comment)
    {
        var commentEntity = new Comment { BookId = comment.BookId, UserId = 1, CreatedAt = DateTime.Now, Content = comment.Content };
        await _commentRepository.AddAsync(commentEntity);
        await _commentRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment != null)
        {
            _commentRepository.Delete(comment);
            await _commentRepository.SaveChangesAsync();
        }
    }
}
