using DecorGearDomain.Data.Base;
using DecorGearDomain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DecorGearDomain.Data.Entities
{
    public class User : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 ký tự")]
        public string Name { get; set; }

        [Phone(ErrorMessage = "Vui lòng nhập đúng chuẩn SĐT bắt đầu bằng 0 có tối đa 10 hoặc 11")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng chuẩn email <a-zA-Z0-9+@gmail.com>")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }

        public string? RefreshToken { get; set; }
        public UserStatus Status { get; set; }

        // Khóa ngoại

        // 1 - n
        public virtual ICollection<Favorite>? Favorites { get; set; }
        public virtual ICollection<FeedBack>? FeedBacks { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<VoucherUser>? VoucherUsers { get; set; } = new List<VoucherUser>();

        // 1 - 1
        public virtual Cart Cart { get; set; }
        public virtual Member Member { get; set; }

        // n - 1
        public virtual Role Role { get; set; }


    }
}
