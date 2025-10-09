using AuthorService.Application.Author.Commands;
using AuthorService.Application.Author.Queries;
using AuthorService.Application.DTO.Author;
using AuthorService.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Author>> Create([FromBody] CreateAuthorCommand author)
        {
            var result = await _mediator.Send(author);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> Get(int page = 1, int pageSize = 20)
        {
            var query = new GetAuthorsQuery(page, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDTO>> GetById(int id)
        {
            var query = new GetAuthorByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
