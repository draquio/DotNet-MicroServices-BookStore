using System.Security.Cryptography;
using BookService.Application.Books.Commands;
using BookService.Application.Books.Queries;
using BookService.Application.DTO.Books;
using BookService.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
            try
            {
                var result = await _mediator.Send(book);
                return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> Get(int page = 1, int pageSize = 10)
        {
            try
            {

            var query = new GetBooksQuery(page, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDTO>> GetById(int id)
        {
            var query = new GetByIdBookQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{guid:Guid}")]
        public async Task<ActionResult<BookDTO>> GetByGuid(Guid guid)
        {
            try
            {
                var query = new GetByGuidBookQuery(guid);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
