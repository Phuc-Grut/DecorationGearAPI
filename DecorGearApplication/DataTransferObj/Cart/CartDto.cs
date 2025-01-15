using DecorGearApplication.DataTransferObj.CartDetail;

namespace DecorGearApplication.DataTransferObj
{
    public class CartDto
    {
        public int CartID { get; set; }

        public int UserID { get; set; }

        public List<CartDetailDto> CartDetails { get; set; } = new List<CartDetailDto> { };
        //public int TotalQuantity => CartDetails.Sum(x => x.Quantity);

        //public double TotalPrice => CartDetails.Sum(x => x.UnitPrice * x.TotalPrice);
        public int TotalQuantity { get; set; }

        // Thêm setter để có thể gán giá trị
        public double TotalAmount { get; set; }
    }
}
