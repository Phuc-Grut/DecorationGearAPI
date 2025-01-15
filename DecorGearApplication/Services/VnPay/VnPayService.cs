//using DecorGearApplication.IServices;
//using DecorGearApplication.Services.Libraries;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DecorGearApplication.Services.VnPay
//{
//    public class VnPayService : IVnPayService
//    {
//        private readonly IConfiguration _configuration;

//        public VnPayService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }


//        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
//        {
//            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
//            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
//            var tick = DateTime.Now.Ticks.ToString();
//            var pay = new VnpayLibrary();
//            var urlCallBack = _configuration["PaymentBackReturnUrl"];

//            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
//            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
//            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
//            //pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
//            pay.AddRequestData("vnp_Amount", ((long)(model.Amount * 100)).ToString());
//            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
//            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
//            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
//            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
//            //pay.AddRequestData("vnp_OrderInfo", $"{model.Name} {model.OrderDescription} {model.Amount}");
//            pay.AddRequestData("vnp_OrderInfo", $"{model.Name} {model.OrderDescription} {model.Amount}".Replace("đ", "d").Replace(" ", " "));
//            pay.AddRequestData("vnp_OrderType", model.OrderType);
//            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
//            pay.AddRequestData("vnp_TxnRef", tick);
//            pay.AddRequestData("vnp_ExpireDate", timeNow.AddMinutes(15).ToString("yyyyMMddHHmmss"));

//            var paymentUrl =
//                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

//            return paymentUrl;
//        }


//        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
//        {
//            var pay = new VnpayLibrary();
//            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

//            return response;
//        }

//    }
//}



using DecorGearApplication.IServices;
using DecorGearApplication.Services.Libraries;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; // Thêm thư viện này để sử dụng ILogger
using System;

namespace DecorGearApplication.Services.VnPay
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        private readonly VnpayLibrary _library;
        private readonly ILogger<VnpayLibrary> _logger; // Khai báo logger

        // Thêm ILogger vào constructor của VnPayService
        public VnPayService(IConfiguration configuration, ILogger<VnpayLibrary> logger)
        {
            _configuration = configuration;
            _logger = logger; // Gán logger vào
            _library = new VnpayLibrary(_logger); // Truyền logger vào khi tạo VnpayLibrary
        }

        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            // var txnRef = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var txnRef = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000, 9999).ToString();


            var returnUrl = _configuration["Vnpay:PaymentBackReturnUrl"];

            _library.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            _library.AddRequestData("vnp_Command", "pay");
            _library.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            _library.AddRequestData("vnp_Amount", ((long)(model.Amount * 100)).ToString());
            _library.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            _library.AddRequestData("vnp_CurrCode", "VND");
            _library.AddRequestData("vnp_IpAddr", _library.GetIpAddress(context));
            _library.AddRequestData("vnp_Locale", "vn");
            _library.AddRequestData("vnp_OrderInfo", ($"{model.Name} {model.OrderDescription} {model.Amount}"));
            _library.AddRequestData("vnp_OrderType", model.OrderType);
            _library.AddRequestData("vnp_ReturnUrl", returnUrl);
            _library.AddRequestData("vnp_TxnRef", txnRef);
            _library.AddRequestData("vnp_ExpireDate", timeNow.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            var paymentUrl = _library.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);
            return paymentUrl;
        }


        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            return _library.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);
        }
    }
}

