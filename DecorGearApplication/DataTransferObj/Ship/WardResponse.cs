using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearApplication.DataTransferObj.Ship
{
    public class WardResponse
    {
        public string WardId { get; set; }

        public string wardName { get; set; }

        public string DistrictId { get; set; }

        public string Type { get; set; }

        public string SupportType { get; set; }

        public string Code { get; set; }

        public List<string> NameExtension { get; set; }

        public string IsEnable { get; set; } 

        public string Status { get; set; } 

        public string PickType { get; set; } 

        public string DeliverType { get; set; }
    }
}
