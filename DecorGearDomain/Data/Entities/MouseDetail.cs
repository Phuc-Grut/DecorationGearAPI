using DecorGearDomain.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecorGearDomain.Data.Entities
{
    public class MouseDetail : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MouseDetailID { get; set; }

        public int ProductID { get; set; }

        // thuộc tính

        public string Color { get; set; } // Màu sắc

        public int DPI { get; set; } // Độ phân giải

        public string Connectivity { get; set; } // Kết nối (ví dụ: USB, Bluetooth)

        public string Dimensions { get; set; } // Kích thước

        public string Material { get; set; } // vật liệu

        public string? EyeReading { get; set; }   //(tần số quét )

        public int? Button { get; set; } // số nút bấm

        public string? LED { get; set; }

        public string? SS { get; set; } // (software support) phần mềm hỗ trợ

        public int? BatteryCapacity { get; set; } // dung lượng pin

        public double Price { get; set; } // giá tiền

        public int View { get; set; } // lượt xem

        // Hỗ trợ cho api giao hàng

        public string Size { get; set; } // kích cỡ hàng hóa

        public int Quantity { get; set; } // số lượng

        public double Weight { get; set; } // cân nặng

        //Khóa ngoại 
        public virtual Product Product { get; set; }
    }
}
