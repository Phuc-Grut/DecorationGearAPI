﻿namespace DecorGearApplication.DataTransferObj.CartDetail
{
    public class UpdateCartDetailRequest
    {
        public int CartDetailID { get; set; }

        public int ProductID { get; set; }

        public int CartID { get; set; }

        public int? OrderID { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }
    }
}
