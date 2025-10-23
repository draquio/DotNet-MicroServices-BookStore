using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Application.Carts.Commands;
using ShoppingCartService.External.Services.Books.Interfaces;
using ShoppingCartService.External.Services.Books;
using ShoppingCartService.Persistence;
using ShoppingCartService.Mappings;
using ShoppingCartService.External.Services.Author.Interfaces;
using ShoppingCartService.External.Services.Author;

namespace ShoppingCartService.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCartDependencies(this IServiceCollection services, IConfiguration config)
        {
            // Mapping
            services.AddAutoMapper(config => config.AddMaps(typeof(MappingProfile).Assembly));


            // DB
            var connectionString = config.GetConnectionString("CartDatabase");
            services.AddDbContext<CartContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            // MediatR
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(
                typeof(CreateCartCommand).Assembly));

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // AuthorService
            var authoURL = config["Services:Authors"] ?? Environment.GetEnvironmentVariable("AUTHORS_BASE_URL") ?? "http://localhost:5101";
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddHttpClient<IAuthorService, AuthorService>(client =>
            {
                client.BaseAddress = new Uri(authoURL);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            // BookService
            services.AddScoped<IBookService, BookService>();
            var bookURL = config["Services:Books"] ?? Environment.GetEnvironmentVariable("BOOKS_BASE_URL") ?? "http://localhost:5102";
            services.AddHttpClient<IBookService, BookService>(client =>
            {
                client.BaseAddress = new Uri(bookURL);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            return services;
        }
    }
}
