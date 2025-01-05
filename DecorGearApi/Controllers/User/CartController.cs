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
    }
}
