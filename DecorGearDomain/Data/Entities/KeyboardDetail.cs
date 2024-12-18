using DecorGearDomain.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace DecorGearDomain.Data.Entities
{
    public class KeyboardDetail : EntityBase
    {
        public int KeyboardDetailID { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public int ProductID { get; set; }
        public string Color { get; set; } // Màu sắc
        public string Layout { get; set; } // bố cục phím 
        public string Case { get; set; }   // vỏ ngoài 
        public string Switch { get; set; } // trục phím
        public int? SwitchLife { get; set; } // tuổi thọ trục (số lần nhấn)
        public string? Led { get; set; }
        public string? KeycapMaterial { get; set; } // chất liệu keycap
        public string? SwitchMaterial { get; set; } // chất liệu switch
        public string? SS { get; set; } // (software support) phần mềm hỗ trợ
        public string? Stabilizes { get; set; } // Phụ kiện cân bằng keycap
        public string? PCB { get; set; } // bảng mạch

        //Khóa ngoại 

        // 1 - 1
        public virtual Product Product { get; set; }

        // 1 - n
    }
}
