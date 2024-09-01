using Library.Repositories.Interfaces;

namespace Library.Repositories.Database.Entities
{
    public class Comment : ISoftDeletable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }
    }
}