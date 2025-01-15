using DecorGearDomain.Enum;
using System.ComponentModel.DataAnnotations;

namespace DecorGearApplication.DataTransferObj.Sale
{
    public class UpdateSaleRequest
    {
        public int SaleID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 ký tự")]
        public string SaleName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập phần trăm giảm giá")]
        [Range(0, 100, ErrorMessage = ("Phần trăm giảm giá không hợp lệ"))]
        public int SalePercent { get; set; }

        [Range(1, 4, ErrorMessage = "Vui lòng lựa chọn từ chọn trạng thái> ")]
        public EntityStatus Status { get; set; }
    }
}
