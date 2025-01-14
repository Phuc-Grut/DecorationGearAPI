using DecorGearDomain.Data.Base;
using DecorGearDomain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecorGearDomain.Data.Entities
{
    public class Voucher : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoucherID { get; set; }

        public string VoucherName { get; set; }

        public double VoucherPercent { get; set; }

        public DateTime expiry { get; set; }

        public EntityStatus Status { get; set; }

        // Khóa ngoại

        // 1 - n

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<VoucherUser>? VoucherUsers { get; set; } = new List<VoucherUser>();
    }
}
