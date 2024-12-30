using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearApplication.DataTransferObj.SubCategory
{
    public class SubCategoryProductDto
    {
        public int? ProductID { get; set; }

        public string? ProductCode { get; set; }

        public string? ProductName { get; set; }

        public int? View { get; set; }

        public string? Description { get; set; }

        public string? AvatarProduct { get; set; }

        public int? SubCategoryID { get; set; }

        public int CategoryID { get; set; }

        public string? SubCategoryName { get; set; }

        public List<string>? ImageProduct { get; set; }

        public object? ProductDetail { get; set; }
    }
}
