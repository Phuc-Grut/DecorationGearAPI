using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DecorGearApplication.DataTransferObj.OrderDetail
{
    public class OrderDetailDTO
    {
        public int OrderDetailId { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public string size { get; set; }

        public double weight { get; set; }

        public int TotalQuantity => Quantity;

        public double OrderDetailPrice => UnitPrice * Quantity;
    }
}
                                    