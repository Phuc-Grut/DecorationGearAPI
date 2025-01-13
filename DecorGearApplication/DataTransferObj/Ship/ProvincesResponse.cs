using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearApplication.DataTransferObj.Shipping
{
    public class ProvincesResponse
    {
        public string ProvinceID { get; set; }

        public string ProvinceName { get; set; }

        public string CountryID { get; set; }

        public string Code { get; set; }

        public List<string> NameExtension { get; set; }

        public string RegionID { get; set; }

        public string IsEnable { get; set; }

        public string AreaID { get; set; }

        public string CanUpdateCOD { get; set; }
    }
}
