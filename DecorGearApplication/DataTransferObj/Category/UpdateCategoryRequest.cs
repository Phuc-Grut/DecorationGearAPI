using System.ComponentModel.DataAnnotations;

namespace DecorGearApplication.DataTransferObj.Category
{
    public class UpdateCategoryRequest
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 ký tự")]
        public string CategoryName { get; set; }
    }
}
