using DecorGearDomain.Enum;
using System.ComponentModel.DataAnnotations;

namespace DecorGearApplication.DataTransferObj.Order
{
    public class CreateOrderRequest
    {
        public int UserID { get; set; }

        [StringLength(100, ErrorMessage = "Không được vượt quá 100 ký tự")]
        public int? VoucherID { get; set; } // 1 oder có tối đa 1 voucher ( có thể có hoặc không nên đẻ ? )

        [Range(1, 9, ErrorMessage = "Vui lòng lựa chọn từ chọn trạng thái> ")]
        public OrderStatus OrderStatus { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(100, ErrorMessage = "Không được vượt quá 100 ký tự")]
        public string paymentMethod { get; set; }

        public DateTimeOffset? CreatedTime { get; set; }

        public DateTime? completeDate { get; set; }
    }
}
