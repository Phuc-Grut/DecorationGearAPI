using DecorGearApplication.DataTransferObj.Ship;
using DecorGearApplication.DataTransferObj.Shipping;
using DecorGearApplication.Interface;
using DecorGearDomain.Data.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace DecorGearInfrastructure.Implement
{
    public class ShippingRespository : IShippingRespository
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://dev-online-gateway.ghn.vn/shiip/public-api/";
        private const string ApiToken = "f7b243b2-cc01-11ef-8b4f-c226320e14e8";


        public ShippingRespository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Token", ApiToken);
        }

        //private async Task<ResponseDelivertDto<object>> HandleApiResponse<T>(HttpResponseMessage response)
        //{
        //    response.EnsureSuccessStatusCode();
        //    var responseData = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<ResponseDelivertDto<object>>(responseData);
        //}

        public async Task<ResponseDelivertDto<List<ProvincesResponse>>> GetProvincesAsync()
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}master-data/province");

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Data: " + responseData);  // Log response data

                // Deserialize dữ liệu thành một danh sách ProvincesResponse
                var responseObj = JsonConvert.DeserializeObject<ResponseProvincesWrapper>(responseData);

                if (responseObj != null && responseObj.Data != null && responseObj.Data.Any())
                {
                    // Trả về danh sách ProvincesResponse
                    return new ResponseDelivertDto<List<ProvincesResponse>>
                    {
                        Data = responseObj.Data,
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Data retrieved successfully"
                    };
                }
                else
                {
                    return new ResponseDelivertDto<List<ProvincesResponse>>
                    {
                        Data = new List<ProvincesResponse>(),  // Trả về danh sách rỗng nếu không có dữ liệu
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No provinces found in response"
                    };
                }
            }
            else
            {
                Console.WriteLine("Failed to get data from API");
                return new ResponseDelivertDto<List<ProvincesResponse>>
                {
                    Data = new List<ProvincesResponse>(),  // Trả về danh sách rỗng nếu có lỗi
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Failed to retrieve data"
                };
            }

            //return await HandleApiResponse<provincesResponseDto>(response);
        }

        public async Task<ResponseDelivertDto<List<DistrictResponse>>> GetDistrictsAsync(int provinceId)
        {
            var requestUrl = $"{ApiBaseUrl}master-data/district";
            var requestData = new { province_id = provinceId };
            var jsonData = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Data: " + responseData);  // Log response data

                // Deserialize dữ liệu thành một danh sách DistrictResponse
                var responseObj = JsonConvert.DeserializeObject<ResponseDistrictsWrapper>(responseData);

                if (responseObj != null && responseObj.Data != null && responseObj.Data.Any())
                {
                    // Trả về danh sách DistrictResponse
                    return new ResponseDelivertDto<List<DistrictResponse>>
                    {
                        Data = responseObj.Data.Cast<DistrictResponse>().ToList(),
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Data retrieved successfully"
                    };
                }
                else
                {
                    return new ResponseDelivertDto<List<DistrictResponse>>
                    {
                        Data = new List<DistrictResponse>(),  // Trả về danh sách rỗng nếu không có dữ liệu
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No districts found in response"
                    };
                }
            }
            else
            {
                Console.WriteLine("Failed to get data from API");
                return new ResponseDelivertDto<List<DistrictResponse>>
                {
                    Data = new List<DistrictResponse>(),  // Trả về danh sách rỗng nếu có lỗi
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Failed to retrieve data"
                };
            }
        }

        public async Task<ResponseDelivertDto<List<WardResponse>>> GetWardsAsync(int districtId)
        {
            var requestUrl = $"{ApiBaseUrl}master-data/ward";
            var requestData = new { district_id = districtId };
            var jsonData = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Data: " + responseData);  // Log response data

                // Deserialize dữ liệu thành một danh sách WardResponse
                var responseObj = JsonConvert.DeserializeObject<ResponseWardsWrapper>(responseData);

                if (responseObj != null && responseObj.Data != null && responseObj.Data.Any())
                {
                    // Trả về danh sách WardResponse
                    return new ResponseDelivertDto<List<WardResponse>>
                    {
                        Data = responseObj.Data.Cast<WardResponse>().ToList(),
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Data retrieved successfully"
                    };
                }
                else
                {
                    return new ResponseDelivertDto<List<WardResponse>>
                    {
                        Data = new List<WardResponse>(),  // Trả về danh sách rỗng nếu không có dữ liệu
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No wards found in response"
                    };
                }
            }
            else
            {
                Console.WriteLine("Failed to get data from API");
                return new ResponseDelivertDto<List<WardResponse>>
                {
                    Data = new List<WardResponse>(),  // Trả về danh sách rỗng nếu có lỗi
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Failed to retrieve data"
                };
            }
        }

    }
}
