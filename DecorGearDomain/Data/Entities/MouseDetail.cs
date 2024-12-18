﻿using DecorGearDomain.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace DecorGearDomain.Data.Entities
{
    public class MouseDetail : EntityBase
    {
        [Required(ErrorMessage = "Không được để trống")]
        public int MouseDetailID { get; set; }
        public int ProductID { get; set; }

        [StringLength(100, ErrorMessage = "Mô tả không được vượt quá 100 ký tự")]
        public string Color { get; set; } // Màu sắc
        public int DPI { get; set; } // Độ phân giải
        public string Connectivity { get; set; } // Kết nối (ví dụ: USB, Bluetooth)
        public string Dimensions { get; set; } // Kích thước
        public string Material { get; set; } // vật liệu
        public string? EyeReading { get; set; }   //(tần số quét )
        public int? Button { get; set; } // số nút bấm
        public string? LED { get; set; }
        public string? SS { get; set; } // (software support) phần mềm hỗ trợ

        //Khóa ngoại 

        // 1 - 1
        public virtual Product Product { get; set; }

        // 1 - n
    }
}
