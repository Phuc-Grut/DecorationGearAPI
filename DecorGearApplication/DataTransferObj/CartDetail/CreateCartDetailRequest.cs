using DecorGearDomain.Enum;

namespace DecorGearApplication.DataTransferObj.CartDetail
{
    public class CreateCartDetailRequest
    {
        public int UserID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public CartStatus Status { get; set; }  
    }
}
