using BookService.Application.Books.Commands;
using BookService.Application.Books.Queries;
using BookService.Application.DTO.Books;
using BookService.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Create([FromBody] CreateBookCommand book)
        {
            var result = await _mediator.Send(book);
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> Get(int page = 1, int pageSize = 10)
        {
            var query = new GetBooksQuery(page, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{guid}")]
        public async Task<ActionResult<BookDTO>> GetByGuid(Guid guid)
        {
            var query = new GetByGuidBookQuery(guid);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
