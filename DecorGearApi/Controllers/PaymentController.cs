using DecorGearApplication.IServices;
using DecorGearApplication.Services.VnPay;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DecorGearApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IVnPayService _vnPayService;

        public PaymentController(IVnPayService vnPayService)
        {

            _vnPayService = vnPayService;
        }
        [HttpPost]
        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }
        [HttpGet]
        public IActionResult PaymentCallbackVnpay()
        {
            try
            {
                // Kiểm tra nếu Request.Query không có dữ liệu
                if (!Request.Query.Any())
                {
                    return BadRequest(new { message = "Không có dữ liệu trong callback từ VNPay." });
                }

                // Xử lý callback từ VNPay
                var response = _vnPayService.PaymentExecute(Request.Query);

                // Trả về kết quả xử lý
                return Ok(response);
            }
            catch (Exception ex)
            {            
            // Trả về lỗi
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi xử lý callback từ VNPay." });
            }
        }


    }
}
