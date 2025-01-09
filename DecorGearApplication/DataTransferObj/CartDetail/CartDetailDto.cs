namespace DecorGearApplication.DataTransferObj.CartDetail
{
    public class CartDetailDto
    {
        public int CartDetailID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public int ProductID { get; set; }

        public int CartID { get; set; }

        public int? OrderID { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrices { get; set; }
    }
}
