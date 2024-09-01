namespace Library.Repositories.Database.Entities;

public class AuthorizationToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime Expiration { get; set; }
    public string Token { get; set; } = null!;
    public bool IsStatic { get; set; } //Флаг статичного токена задается для админа в рамках тестов, чтобы не проверять срок жизни токена и не переавторизовываться
}