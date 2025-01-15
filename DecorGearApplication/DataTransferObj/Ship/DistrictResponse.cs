using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearApplication.DataTransferObj.Ship
{
    public class DistrictResponse
    {
        public string DistrictID { get; set; }

        public string ProvinceID { get; set; }

        public string DistrictName { get; set; }    

        public string Code { get; set; }

        public List<string> NameExtension { get; set; }

        public string Type { get; set; }

        public string SupportType { get; set; }

        public string PickType { get; set; }

        public string DeliverType { get; set; }

        public List<string> WhiteListDistrict { get; set; }

        public List<string> WhiteListClient { get; set; }

        public string Status { get; set; }
    }
}
