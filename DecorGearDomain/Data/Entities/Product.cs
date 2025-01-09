using DecorGearDomain.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecorGearDomain.Data.Entities
{
    public class Product : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        [StringLength(10)]
        public string ProductCode { get; set; }

        public int? SaleID { get; set; }
        public int BrandID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(255, ErrorMessage = "Không vượt quá 255 ký tự")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Giá phải lớn hơn 0 ")]

        public int View { get; set; }
        public string? Description { get; set; }
        public string? AvatarProduct { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Đơn vị dung lượng pin được dung ở đây là Miliample/Hour")]

        // Khóa ngoại 

        // 1 - n
        public virtual ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>();
        public virtual ICollection<CartDetail>? CartDetails { get; set; } = new List<CartDetail>();
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public virtual ICollection<FeedBack>? FeedBacks { get; set; } = new List<FeedBack>();
        public virtual ICollection<MouseDetail> MouseDetails { get; set; } = new List<MouseDetail>();
        public virtual ICollection<KeyboardDetail> KeyboardDetails { get; set; } = new List<KeyboardDetail>();

        // n - 1
        public virtual Sale Sale { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<ImageList>? ImageLists { get; set; } = new List<ImageList>();
        public virtual ICollection<ProductSubCategory> ProductSubCategories { get; set; } = new List<ProductSubCategory>();
    }
}
