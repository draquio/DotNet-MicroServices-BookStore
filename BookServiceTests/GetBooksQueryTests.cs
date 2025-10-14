
using AutoMapper;
using BookService.Application.Books.Queries;
using BookService.Application.DTO.Books;
using BookService.Mappings;
using BookService.Models;
using BookService.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookServiceTests
{
    public class GetBooksQueryTests
    {
        private static IMapper CreateMapper()
        {
            var loggerFactory = LoggerFactory.Create(b => {});
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }, loggerFactory);

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
        private BookContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BookContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new BookContext(options);
            context.AddRange(GetTestData());
            context.SaveChanges();
            return context;
        }

        private List<Book> GetTestData()
        {
            return new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Book 1",
                    PublishDate = new DateOnly(2008, 8, 1),
                    BookGuid = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    AuthorGuid = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
                },
                new Book
                {
                    Id = 2,
                    Title = "Book 2",
                    PublishDate = new DateOnly(1994, 10, 15),
                    BookGuid = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    AuthorGuid = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")
                },
                new Book
                {
                    Id = 3,
                    Title = "Book 3",
                    PublishDate = null,
                    BookGuid = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    AuthorGuid = null
                }
            };
        }


        [Fact]
        public async Task GetAllBooks_ReturnTwoDTOs()
        {
            using var context = CreateInMemoryContext();
            var mapper = CreateMapper();
            var handler = new GetBooksQueryHandler(context, mapper);
            var query = new GetBooksQuery(Page: 1, Pagesize: 2);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<List<BookDTO>>(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, b => b.Title == "Book 1");
            Assert.Contains(result, b => b.Title == "Book 2");
        }

        [Fact]
        public async Task GetAllBooks_ReturnOneDTO()
        {
            using var context = CreateInMemoryContext();
            var mapper = CreateMapper();
            var handler = new GetBooksQueryHandler(context, mapper);
            var query = new GetBooksQuery(Page: 2, Pagesize: 2);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Book 3", result[0].Title);
        }
        [Fact]
        public async Task GetAllBooks_ReturnEmpty()
        {
            using var context = CreateInMemoryContext();
            var mapper = CreateMapper();
            var handler = new GetBooksQueryHandler(context, mapper);
            var query = new GetBooksQuery(Page: 10, Pagesize: 5);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
