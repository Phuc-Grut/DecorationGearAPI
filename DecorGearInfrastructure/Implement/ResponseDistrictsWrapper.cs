using DecorGearApplication.DataTransferObj.Ship;
using DecorGearApplication.DataTransferObj.Shipping;

namespace DecorGearInfrastructure.Implement
{
    public class ResponseDistrictsWrapper
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<DistrictResponse> Data { get; set; }
    }
}