namespace DecorGearApplication.DataTransferObj.MouseDetails
{
    public class MouseDetailsDto
    {
        public int MouseDetailID { get; set; }

        //public int ProductID { get; set; }

        public string? Color { get; set; }
        public double? Price { get; set; }
        public string? Switch { get; set; }
        public int? Quantity { get; set; }
        public int? DPI { get; set; }

        public string? Connectivity { get; set; }

        public string? Dimensions { get; set; }

        public string? Material { get; set; } 

        public string? EyeReading { get; set; }

        public int? Button { get; set; }

        public string? LED { get; set; }

        public string? SS { get; set; }
        public string? Size { get; set; }
        public int? BatteryCapacity { get; set; } // dung lượng pin
        public double? Weight { get; set; }
    }
}
