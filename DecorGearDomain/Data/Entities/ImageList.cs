﻿using DecorGearDomain.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecorGearDomain.Data.Entities
{
    public class ImageList : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageListID { get; set; }

        //public int? MouseDetailID { get; set; }

        //public int? KeyboardDetailID { get; set; }
        public int? ProductID { get; set; }
        public string ImagePath { get; set; }

        [StringLength(500, ErrorMessage = "Không được vượt quá 500 ký tự")]
        public string? Description { get; set; }

        //Khóa ngoại

        // n - 1
        //public virtual MouseDetail MouseDetail { get; set; }

        //public virtual KeyboardDetail KeyboardDetail { get; set; }
        public virtual Product Product { get; set; }
    }
}
