using DecorGearApplication.DataTransferObj.Ship;
using DecorGearApplication.DataTransferObj.Shipping;

namespace DecorGearInfrastructure.Implement
{
    public class ResponseWardsWrapper
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<WardResponse> Data { get; set; }
    }
}