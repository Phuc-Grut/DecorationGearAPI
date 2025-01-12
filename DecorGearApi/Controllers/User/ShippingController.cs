using DecorGearApplication.DataTransferObj;
using DecorGearApplication.DataTransferObj.Ship;
using DecorGearApplication.DataTransferObj.Shipping;
using DecorGearApplication.Interface;
using DecorGearApplication.ValueObj.Response;
using DecorGearDomain.Data.Entities;
using DecorGearInfrastructure.Implement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DecorGearApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        public readonly IShippingRespository _svShiping;


        public ShippingController(IShippingRespository shippingRespository)
        {
            _svShiping = shippingRespository;
        }

        // GET: api/shipping/provinces
        [HttpGet("provinces")]
        public async Task<IActionResult> GetProvinces()
        {
            try
            {
                var response = await _svShiping.GetProvincesAsync();

                if (response == null || response.StatusCode != StatusCodes.Status200OK || response.Data == null)
                {
                    return StatusCode(500, new ResponseDto<ProvincesResponse>(null, 500, "Không thể lấy dữ liệu tỉnh thành phố"));
                }

                return Ok(new ResponseDto<List<ProvincesResponse>>(response.Data, 200, "Lấy dữ liệu tỉnh thành phố thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<ProvincesResponse>(null, 500, $"Lỗi khi lấy dữ liệu tỉnh thành phố: {ex.Message}"));
            }
        }

        // GET: api/shipping/districts/{provinceId}
        [HttpGet("districts/{provinceId}")]
        public async Task<IActionResult> GetDistricts(int provinceId)
        {
            try
            {
                var response = await _svShiping.GetDistrictsAsync(provinceId);

                if (response == null || response.StatusCode != StatusCodes.Status200OK)
                {
                    return StatusCode(500, new ResponseDto<object>(null, 500, "Không thể lấy dữ liệu quận huyện"));
                }

                return Ok(new ResponseDto<List<DistrictResponse>>(response.Data, 200, "Lấy dữ liệu quận huyện thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<object>(null, 500, $"Lỗi khi lấy dữ liệu quận huyện: {ex.Message}"));
            }
        }

        // GET: api/shipping/wards/{districtId}
        [HttpGet("wards/{districtId}")]
        public async Task<IActionResult> GetWards(int districtId)
        {
            try
            {
                var response = await _svShiping.GetWardsAsync(districtId);

                if (response == null)
                {
                    return StatusCode(500, new ResponseDto<object>(null, 500, "Không thể lấy dữ liệu phường xã"));
                }

                return Ok(new ResponseDto<List<WardResponse>>(response.Data, 200, "Lấy dữ liệu phường xã thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<object>(null, 500, $"Lỗi khi lấy dữ liệu phường xã: {ex.Message}"));
            }
        }
    }
}
