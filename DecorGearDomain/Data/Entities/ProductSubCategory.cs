using DecorGearDomain.Data.Base;

namespace DecorGearDomain.Data.Entities
{
    public class ProductSubCategory : EntityBase
    {
        public int ProductID { get; set; } // Khóa ngoại

        public Product Product { get; set; }

        public int SubCategoryID { get; set; } // Khóa ngoại

        public SubCategory SubCategory { get; set; }
    }
}