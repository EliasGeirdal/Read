using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Read.Application.Context;
using Read.Application.Models;
using Read.Application.Repository.Book;
using Read.Application.Services.Auth;
using Read.Application.Services.Book;

namespace Read.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ReadContext>(options => { options.UseSqlServer(connectionString); });

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}