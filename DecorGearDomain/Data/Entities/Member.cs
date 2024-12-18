using DecorGearDomain.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace DecorGearDomain.Data.Entities
{
    public class Member : EntityBase
    {
        [Required(ErrorMessage = "Không được để trống")]
        public int MemberID { get; set; }
        public int UserID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Phải là số dương")]
        public int Points { get; set; }
        public DateTime ExpiryDate { get; set; }

        // Khóa ngoại

        // 1 - 1
        public virtual User User { get; set; }
    }
}
