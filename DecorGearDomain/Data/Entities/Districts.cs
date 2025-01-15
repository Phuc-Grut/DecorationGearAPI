using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearDomain.Data.Entities
{
    public class Districts
    {
        public int DistrictId { get; set; }

        public string DistrictName { get; set; }

        public int ProvinceId { get; set; }

        // Khóa ngoại 

        // n - 1

        public virtual Provinces Provinces { get; set; } 

        // 1 - n 

        public virtual ICollection<CustomerAddresses> CustomerAddresses { get; set; } = new List<CustomerAddresses>();

        public virtual ICollection<Wards> Wards { get; set; } = new List<Wards>();
    }
}
