using Library.Repositories.Database.Entities;

namespace Library.Repositories.Interfaces;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<List<Book>> GetAllAsync();
}