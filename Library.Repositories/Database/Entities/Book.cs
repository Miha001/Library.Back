using Library.Repositories.Interfaces;

namespace Library.Repositories.Database.Entities;

public class Book : ISoftDeletable
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime? PublishedDate { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<FavoriteBook> FavoriteBooks { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
