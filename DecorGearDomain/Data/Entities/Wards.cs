using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearDomain.Data.Entities
{
    public class Wards
    {
        public int WardId { get; set; }

        public string wardName { get; set; }

        public int DistrictId { get; set; }

        // Khóa ngoại 

        // n - 1

        public virtual Districts Districts { get; set; }

        // 1 - n 

        public virtual ICollection<CustomerAddresses> CustomerAddresses { get; set; }
    }
}
