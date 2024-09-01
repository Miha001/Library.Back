using Library.Core.Models;
using Library.Repositories.Database.Entities;

namespace Library.Services.Interfaces;

public interface IBookService
{
    Task<List<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(int id);
    Task AddAsync(AddBookRequest request);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);
}