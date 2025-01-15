using DecorGearApplication.DataTransferObj.User;
using DecorGearApplication.DataTransferObj.Voucher;
using DecorGearApplication.Interface;
using DecorGearDomain.Data.Entities;
using DecorGearDomain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace DecorGearApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherRespository _voucherRepo;
        public VoucherController(IVoucherRespository voucherRespository)
        {
            _voucherRepo = voucherRespository;
        }
        [HttpGet("get-all-voucher")]
        public async Task<ActionResult<IEnumerable<VoucherDto>>> GetAllVoucher([FromQuery] VoucherSearch? voucherSearch, CancellationToken cancellationToken)
        {
            var voucher = await _voucherRepo.GetAllVoucher(voucherSearch, cancellationToken);
            return Ok(voucher);
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<VoucherDto>> GetByIdVoucher(int id, CancellationToken cancellationToken)
        {
            var voucher = await _voucherRepo.GetKeyVoucherById(new ViewVoucherRequest { VoucherID = id }, cancellationToken);
            return Ok(voucher);
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ResponseDto<Voucher>>> PutVoucher([FromBody] UpdateVoucherRequest request, int id, CancellationToken cancellationToken)
        {
            // Kiểm tra xem yêu cầu có null hay không
            if (request == null)
            {
                return BadRequest(new ResponseDto<Voucher>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Request is null"
                });
            }

            // Cập nhật voucher
            var response = await _voucherRepo.UpdateVoucher(id, request, cancellationToken);

            // Kiểm tra xem voucher có được cập nhật thành công hay không
            if (response.Status == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }

            // Trả về kết quả cập nhật thành công
            return Ok(response);
        }
        [HttpPost("add")]
        public async Task<ActionResult> PostVoucher([FromBody] CreateVoucherRequest request, CancellationToken cancellationToken)
        {
            var voucher = await _voucherRepo.CreateVoucher(request, cancellationToken);
            return Ok(voucher);
        }
        //[HttpDelete("delete/{id}")]
        //public async Task<ActionResult> DeleteVoucher(int id, CancellationToken cancellationToken)
        //{
        //    // Tạo yêu cầu để xóa voucher
        //    var request = new DeleteVoucherRequest { VoucherID = id };

        //    // Gọi phương thức xóa voucher với cả hai tham số
        //    var voucher = await _voucherRepo.DeleteVoucher(id, request, cancellationToken);

        //    return Ok(voucher);
        //}
    }
}