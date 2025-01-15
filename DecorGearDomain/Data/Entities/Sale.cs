using DecorGearDomain.Data.Base;
using DecorGearDomain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecorGearDomain.Data.Entities
{
    public class Sale : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleID { get; set; }

        public string SaleName { get; set; }

        public int SalePercent { get; set; } 

        public EntityStatus Status { get; set; }


        // Khóa ngoại

        // 1 - n
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
