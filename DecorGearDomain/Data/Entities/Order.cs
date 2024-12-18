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
        public int? VoucherID { get; set; } // 1 oder có tối đa 1 voucher ( có thể có hoặc không nên đẻ ? )

        [Required(ErrorMessage = "Không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int totalQuantity { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Tổng giá phải là giá trị dương")]
        public double totalPrice { get; set; }
        public OrderStatus Status { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public string paymentMethod { get; set; }
        public string size { get; set; }
        public float weight { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public DateTime OrderDate { get; set; } // ngày giao hàng

        // Khóa ngoại

        // 1 - n
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();

        // n - 1
        public virtual Voucher Voucher { get; set; }

        public virtual User User { get; set; }
    }
}
