using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartService.Application.Carts.Commands;
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
            var result = await _mediator.Send(cart);
            return CreatedAtAction(nameof(Create), new { id = result.Id}, result);
        }
    }
}
