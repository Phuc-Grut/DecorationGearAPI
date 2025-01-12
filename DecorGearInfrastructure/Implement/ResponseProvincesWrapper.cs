using DecorGearApplication.DataTransferObj.Shipping;

namespace DecorGearInfrastructure.Implement
{
    public class ResponseProvincesWrapper
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<ProvincesResponse> Data { get; set; }
    }
}