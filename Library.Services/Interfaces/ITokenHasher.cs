namespace Library.Services.Interfaces;

public interface ITokenHasher
{
    string HashToken(string password, string salt);
}