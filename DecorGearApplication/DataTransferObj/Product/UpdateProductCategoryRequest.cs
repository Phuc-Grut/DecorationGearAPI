using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearApplication.DataTransferObj.Product
{
    public class UpdateProductCategoryRequest
    {
        public int CategoryID { get; set; } // ID của Category mới
        public List<int> SubCategoryIDs { get; set; } // Danh sách các SubCategory ID
    }
}
