using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartService.Application.Carts.Commands;
using ShoppingCartService.Application.Carts.Queries;
using ShoppingCartService.Application.DTO.Carts;
using ShoppingCartService.Models;

namespace ShoppingCartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<CartDTO>> Create([FromBody] CreateCartCommand cart)
        {
            try
            {
                var result = await _mediator.Send(cart);
                return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<CartDTO>>> Get(int page = 1, int pageSize = 10)
        {
            try
            {
                var query = new GetCartsQuery(page, pageSize);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartDTO>> GetById(int id)
        {
            try
            {
                var query = new GetByIdCartQuery(id);
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
