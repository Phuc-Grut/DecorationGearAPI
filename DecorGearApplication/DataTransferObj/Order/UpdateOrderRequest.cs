﻿using DecorGearDomain.Enum;

namespace DecorGearApplication.DataTransferObj.Order
{
    public class UpdateOrderRequest
    {
        public string UserID { get; set; }

        public int totalQuantity { get; set; }

        public decimal totalPrice { get; set; }

        public string paymentMethod { get; set; }

        public float size { get; set; }

        public float weight { get; set; }

        public DateTime OrderDate { get; set; }
        public int? VoucherID { get; set; }
        public OrderStatus Status { get; set; }
    }
}
