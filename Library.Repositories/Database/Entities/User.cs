using Library.Repositories.Interfaces;

namespace Library.Repositories.Database.Entities;

public class User : ISoftDeletable
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string MiddleName { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public bool IsDeleted { get; set; }
    public string Email { get; set; }
    

    public ICollection<FavoriteBook> FavoriteBooks { get; set; }
    public ICollection<Comment> Comments { get; set; }
}

