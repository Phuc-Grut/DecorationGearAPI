using DecorGearDomain.Data.Base;
using DecorGearDomain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecorGearDomain.Data.Entities
{
    public class Order : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public int UserID { get; set; }

        public int? VoucherID { get; set; } 

        public OrderStatus OrderStatus { get; set; }

        public string paymentMethod { get; set; }

        public DateTime OrderDate { get; set; } // thời gian hoàn thành thanh toán

        // Khóa ngoại

        // 1 - n
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();

        // n - 1
        public virtual Voucher Voucher { get; set; }

        public virtual User User { get; set; }
    }
}
