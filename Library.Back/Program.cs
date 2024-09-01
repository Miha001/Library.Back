using Library.Core.Configuration;
using Library.Services.Extensions;
using Library.Services.Handlers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));
builder.Services.AddControllers();
// Adding Authentication
builder.Services.AddAuthentication(AuthentificationKeys.DefaultSchemeName)
    .AddScheme<DatabaseTokenAuthOptions, DatabaseTokenAuthHandler>
    (AuthentificationKeys.DefaultSchemeName, opt => { });

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.API", Version = "v1" });

    options.AddSecurityDefinition("Token", new OpenApiSecurityScheme()
    {
        Name = AuthentificationKeys.UserAuthTokenKey,
        Type = SecuritySchemeType.ApiKey,
        Scheme = AuthentificationKeys.DefaultSchemeName,
        In = ParameterLocation.Header,
        Description = $"Database token Authorization header using the {AuthentificationKeys.DefaultSchemeName} scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Token"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigureServices(config);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();