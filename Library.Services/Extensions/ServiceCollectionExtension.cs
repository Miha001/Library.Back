using Library.Repositories.Database;
using Library.Repositories.Implementations;
using Library.Repositories.Interfaces;
using Library.Services.Implementations;
using Library.Services.Interfaces;
using LibraryService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Services.Extensions;

public static class ServiceCollectionExtension
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LibraryContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthorizationTokenRepository, AuthorizationTokenRepository>();
        services.AddScoped<IPasswordHasher, IdentityPasswordHashService>();
        services.AddScoped<ITokenHasher, TokenHashSha512Service>();
        services.AddScoped<IAuthorizationTokenService, AuthorizationTokenService>();
    }
}