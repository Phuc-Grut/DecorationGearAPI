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

        [HttpPost("create-payment-url")]
        public IActionResult CreatePaymentUrl([FromBody] PaymentInformationModel model)
        {
            if (model == null || model.Amount <= 0)
            {
                return BadRequest(new { message = "Invalid payment information." });
            }

            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Ok(new { paymentUrl = url });
        }

        [HttpGet("payment-callback")]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            // Tùy thuộc vào logic xử lý, bạn có thể lưu thông tin giao dịch hoặc gửi trạng thái giao dịch
            if (!response.Success)
            {
                return BadRequest(new { message = "Payment failed.", response });
            }

            return Ok(new { message = "Payment successful.", response });
        }
    }
}
