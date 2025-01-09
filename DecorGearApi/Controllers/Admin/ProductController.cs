using AutoMapper;
using DecorGearApplication.DataTransferObj.Product;
using DecorGearApplication.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DecorGearApi.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRespository _res;
        private readonly IMapper _mapper;
        public ProductController(IProductRespository respo, IMapper mapper)
        {
            _res = respo;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery]ViewProductRequest? request, CancellationToken cancellationToken)
        {
            var result = await _res.GetAllProduct(request,cancellationToken);
            return Ok(result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _res.GetKeyProductById(id, cancellationToken);

            if (result == null )
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(result);
        }

        // POST api/<ProductController>
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromQuery]CreateProductRequest request, CancellationToken cancellationToken)
        {
            // Kiểm tra nếu ModelState không hợp lệ
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _res.CreateProduct(request, cancellationToken);
            return Ok(result);
        }

        // PUT api/<ProductController>/5
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct(int id, [FromQuery]UpdateProductRequest request, CancellationToken cancellationToken)
        {
            // Kiểm tra nếu ModelState không hợp lệ
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy sản phẩm cần cập nhật theo ID
            var valueId = await GetById(id, cancellationToken);
            if (valueId == null)
            {
                return NotFound("Không có giá trị ID");
            }

            // Gọi phương thức Update để lưu các thay đổi
            var result = await _res.UpdateProduct(id, request, cancellationToken);

            // Trả về kết quả thành công với sản phẩm đã cập nhật
            return Ok(result);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            // Lấy sản phẩm cần xóa theo ID
            var valueId = await _res.GetKeyProductById(id, cancellationToken);
            if (valueId == null)
            {
                return NotFound("Không có giá trị ID");
            }

            // Gọi phương thức Delete để xóa sản phẩm
            await _res.DeleteProduct(id, cancellationToken);

            // Trả về kết quả thành công với thông báo xác nhận        
            return Ok(valueId);
        }

        // PUT api/<ProductController>/update-category-product
        [HttpPut("update-category-product")]
        public async Task<IActionResult> UpdateCategoryProduct(int productId, [FromBody] UpdateProductCategoryRequest request, CancellationToken cancellationToken)
        {
            if (request == null || request.CategoryID <= 0 || request.SubCategoryIDs == null || !request.SubCategoryIDs.Any())
            {
                return BadRequest("Thông tin không hợp lệ.");
            }
            var result = await _res.UpdateCategoryProduct(productId, request, cancellationToken);

            if (result == null)
            {
                return NotFound($"Sản phẩm với ID {productId} không tồn tại.");
            }

            if (!result.DataResponse)
            {
                return StatusCode(result.Status, result.Message);
            }

            return Ok(new
            {
                Message = "Cập nhật danh mục và danh mục con thành công.",
                Data = result.DataResponse
            });
        }

    }
}
