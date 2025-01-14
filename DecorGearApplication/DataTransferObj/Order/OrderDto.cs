using DecorGearApplication.DataTransferObj.OrderDetail;
using DecorGearDomain.Data.Entities;
using DecorGearDomain.Enum;
using System.ComponentModel.DataAnnotations;

namespace DecorGearApplication.DataTransferObj.Order
{
    public class OrderDto
    {
        public int OrderID { get; set; }

        public int UserID { get; set; }

        public int? VoucherID { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string paymentMethod { get; set; }

        public DateTime completeDate { get; set; }

        public double totalPrice { get; set; }

        public int totalQuantity { get; set; }

        public string size { get; set; }

        public double weight { get; set; }

        public DateTimeOffset? CreatedTime { get; set; }

        public List<OrderDetailDTO> orderDetailDTOs { get; set; } = new List<OrderDetailDTO>();
    }
}
