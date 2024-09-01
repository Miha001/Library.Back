using Library.Core.Models;
using Library.Repositories.Database.Entities;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;

namespace Library.Services.Implementations;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(AddBookRequest request)
    {
        await _bookRepository.AddAsync(new Book { Author = request.Author, PublishedDate = DateTime.UtcNow, Title = request.Title });
        await _bookRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        _bookRepository.Update(book);
        await _bookRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book != null)
        {
            _bookRepository.Delete(book);
            await _bookRepository.SaveChangesAsync();
        }
    }
}