using Microsoft.EntityFrameworkCore;
using AuthorService.Persistence;
using AuthorService.Application.Author.Commands;
using FluentValidation;
using AuthorService.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(config => config.AddMaps(typeof(MappingProfile).Assembly));

// Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthorCommand>();

// DB
builder.Services.AddDbContext<AuthorContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("AuthorDatabase"));
});

// MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(
    typeof(CreateAuthorCommand).Assembly));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "Author Service"));
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
