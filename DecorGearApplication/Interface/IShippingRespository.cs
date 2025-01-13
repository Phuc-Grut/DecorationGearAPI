using DecorGearApplication.DataTransferObj.Ship;
using DecorGearApplication.DataTransferObj.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearApplication.Interface
{
    public interface IShippingRespository
    {
        public Task<ResponseDelivertDto<List<ProvincesResponse>>> GetProvincesAsync();

        public Task<ResponseDelivertDto<List<DistrictResponse>>> GetDistrictsAsync(int provinceId);

        public Task<ResponseDelivertDto<List<WardResponse>>> GetWardsAsync(int districtId);
    }
}
