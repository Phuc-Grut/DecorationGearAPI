using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearApplication.DataTransferObj.Category
{
    public class CategoryProductDto
    {
        public int? ProductID { get; set; }

        public string? ProductCode { get; set; }

        public string? ProductName { get; set; }

        public int? View { get; set; } 

        public string? Description { get; set; }

        public string? AvatarProduct { get; set; }

        public int? CategoryID { get; set; }

        public string? CategoryName { get; set; }

        public List<string>? ImageProduct { get; set; }

        public List<string> SubCategories { get; set; }

        public object? ProductDetail { get; set; }
    }
}
