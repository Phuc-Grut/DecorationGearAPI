using DecorGearApplication.DataTransferObj.CartDetail;
using DecorGearApplication.Interface;
using DecorGearDomain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DecorGearApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ICartRespository _cartRepository;

        public CartController(ICartRespository cartRepository)
        {
            _cartRepository = cartRepository;
        }
         
        [HttpPost("add")]
        public async Task<IActionResult> AddProductToCart([FromBody] CreateCartDetailRequest request, CancellationToken cancellationToken)
        {
            var result = await _cartRepository.AddProductToCart(request, cancellationToken);
            if (result == ErrorMessage.Successfull)
            {
                return Ok(new { message = "Product added to cart successfully" });
            }
            return BadRequest(new { message = "Failed to add product to cart" });
        }

        /// <summary>
        /// Get by user id
        /// </summary>
        /// <returns></returns>
        [HttpGet("user/{id}")]
        public IActionResult GetByUserId([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok(_cartRepository.GetCartById(id, cancellationToken));
        }

        [HttpDelete("user/{userId}/clear")]
        public async Task<IActionResult> ClearCart([FromRoute] int userId, CancellationToken cancellationToken)
        {
            var result = await _cartRepository.ClearCart(userId, cancellationToken);
            if (result == ErrorMessage.Successfull)
            {
                return Ok(new { message = "Cart cleared successfully" });
            }
            return BadRequest(new { message = "Failed to clear cart" });
        }

        [HttpDelete("user/{userId}/product/{productId}")]
        public async Task<IActionResult> DeleteProductFromCart([FromRoute] int userId, [FromRoute] int productId, CancellationToken cancellationToken)
        {
            var result = await _cartRepository.DeleteProductFromCart(userId, productId, cancellationToken);
            if (result == ErrorMessage.Successfull)
            {
                return Ok(new { message = "Product removed from cart successfully" });
            }
            return BadRequest(new { message = "Failed to remove product from cart" });
        }
    }
}
