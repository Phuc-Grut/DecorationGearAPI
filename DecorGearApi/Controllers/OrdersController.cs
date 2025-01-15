using DecorGearApplication.DataTransferObj.Order;
using DecorGearApplication.Interface;
using DecorGearDomain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace DecorGearApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOderRespository _orderRepo;

        public OrdersController(IOderRespository oderRespository)
        {
            _orderRepo = oderRespository;
        }

        // GET: api/Orders
        [HttpGet("get-all-order")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders(CancellationToken cancellationToken)
        {
            var orders = await _orderRepo.GetAllOder(cancellationToken);
            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("get-all-order-detail")]
        public async Task<ActionResult<OrderDto>> GetByIdOrder(int id, CancellationToken cancellationToken)
        {
            var result = await _orderRepo.GetKeyOderById(id, cancellationToken);
            if (result == null)
            {
                return NotFound("Không có giá trị ID");
            }

            return Ok(result);
        }

        [HttpDelete("delete-order")]
        public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
        {
            // Lấy sản phẩm cần xóa theo ID
            var valueId = await _orderRepo.GetKeyOderById(id, cancellationToken);
            if (valueId == null)
            {
                return NotFound("Không có giá trị ID");
            }

            // Gọi phương thức Delete để xóa sản phẩm
            await _orderRepo.DeleteOder(id, cancellationToken);

            // Trả về kết quả thành công với thông báo xác nhận        
            return Ok(valueId);
        }
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return BadRequest("Invalid order request.");
            }

            // Kiểm tra nếu ModelState không hợp lệ
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderRepo.CreateOder(request, cancellationToken);
            return Ok(result);
        }
        [HttpPut("update-order")]
        public async Task<IActionResult> PutOder([FromBody] UpdateOrderRequest request, int id, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return BadRequest("Invalid order request.");
            }
            else
            {
                // Kiểm tra nếu ModelState không hợp lệ
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Lấy sản phẩm cần cập nhật theo ID
                var valueId = await GetByIdOrder(id, cancellationToken);
                if (valueId == null)
                {
                    return NotFound("Không có giá trị ID");
                }

                // Gọi phương thức Update để lưu các thay đổi
                var result = await _orderRepo.UpdateOder(id, request, cancellationToken);

                // Trả về kết quả thành công với sản phẩm đã cập nhật
                return Ok(result);
            }
        }
    }
}
