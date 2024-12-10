using DecorGearApplication.DataTransferObj.ImageList;
using DecorGearApplication.DataTransferObj.KeyBoardDetails;
using DecorGearApplication.DataTransferObj.MouseDetails;
using DecorGearApplication.DataTransferObj.OrderDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DecorGearApplication.DataTransferObj.Product
{
    public class ProductDto
    {
        public int? ProductID { get; set; }

        public int? SaleID { get; set; }

        public int? SubCategoryID { get; set; }

        public string? BrandName { get; set; }

        public int? SaleCode { get; set; }

        public string? ProductName { get; set; }

        public double? Price { get; set; }

        public int? View { get; set; }

        public int? Quantity { get; set; }

        public double? Weight { get; set; }

        public string? Description { get; set; }

        public string? Size { get; set; }

        public int? BatteryCapacity { get; set; } // dung lượng pin
        public string? Category { get; set; }
        public string? AvatarProduct { get; set; }
        public List<string>? ImageProduct { get; set; }
        public object? ProductDetail { get; set; }
    }
}
