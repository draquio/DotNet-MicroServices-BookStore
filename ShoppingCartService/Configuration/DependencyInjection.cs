using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Application.Carts.Commands;
using ShoppingCartService.External.Services.Books.Interfaces;
using ShoppingCartService.External.Services.Books;
using ShoppingCartService.Persistence;
using ShoppingCartService.Mappings;

namespace ShoppingCartService.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCartDependencies(this IServiceCollection services, IConfiguration config)
        {
            // Mapping
            services.AddAutoMapper(config => config.AddMaps(typeof(MappingProfile).Assembly));

            // Services
            services.AddScoped<IBookService, BookService>();

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


            // Http client
            var baseUrl = config["Services:Books"]
                    ?? throw new InvalidOperationException("Config 'Services:Books' required");
            services.AddHttpClient<IBookService, BookService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });
            return services;
        }
    }
}
