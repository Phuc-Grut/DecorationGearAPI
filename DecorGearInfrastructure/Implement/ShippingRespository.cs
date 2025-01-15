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
    public class ShippingRepository : IShippingRespository
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://dev-online-gateway.ghn.vn/shiip/public-api/";
        private const string ApiToken = "f7b243b2-cc01-11ef-8b4f-c226320e14e8";

        public ShippingRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Token", ApiToken);
        }

        private async Task<ResponseDelivertDto<T>> HandleApiResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Data: " + responseData);

                var responseObj = JsonConvert.DeserializeObject<ResponseWrapper<T>>(responseData);
                if (responseObj != null && responseObj.Data != null)
                {
                    return new ResponseDelivertDto<T>
                    {
                        Data = responseObj.Data,
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Data retrieved successfully"
                    };
                }

                return new ResponseDelivertDto<T>
                {
                    Data = default,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No data found in response"
                };
            }

            Console.WriteLine("Failed to get data from API: " + response.ReasonPhrase);
            return new ResponseDelivertDto<T>
            {
                Data = default,
                StatusCode = (int)response.StatusCode,
                Message = "Failed to retrieve data"
            };
        }

        public async Task<ResponseDelivertDto<List<ProvincesResponse>>> GetProvincesAsync()
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}master-data/province");
            return await HandleApiResponse<List<ProvincesResponse>>(response);
        }

        public async Task<ResponseDelivertDto<List<DistrictResponse>>> GetDistrictsAsync(int provinceId)
        {
            var requestUrl = $"{ApiBaseUrl}master-data/district";
            var requestData = new { province_id = provinceId };
            var jsonData = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, content);
            return await HandleApiResponse<List<DistrictResponse>>(response);
        }

        public async Task<ResponseDelivertDto<List<WardResponse>>> GetWardsAsync(int districtId)
        {
            var requestUrl = $"{ApiBaseUrl}master-data/ward";
            var requestData = new { district_id = districtId };
            var jsonData = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, content);
            return await HandleApiResponse<List<WardResponse>>(response);
        }
    }

    public class ResponseWrapper<T>
    {
        public T Data { get; set; }
    }
}
