using DecorGearDomain.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecorGearDomain.Data.Entities
{
    public class SubCategory : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCategoryID { get; set; }

        public string SubCategoryName { get; set; }

        public int CategoryID { get; set; }

        // Khóa ngoại

        // n - 1
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductSubCategory> ProductSubCategories { get; set; } = new List<ProductSubCategory>();
    }
}