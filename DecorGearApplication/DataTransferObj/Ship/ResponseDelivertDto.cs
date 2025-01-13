using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearApplication.DataTransferObj.Ship
{
    public class ResponseDelivertDto<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ResponseDelivertDto()
        {
            Data = default;
            StatusCode = 200;  // Mặc định là 200 OK
            Message = "Success"; // Thông điệp mặc định
        }

        public ResponseDelivertDto(T data, int statusCode, string message)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
        }

        public ResponseDelivertDto(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
