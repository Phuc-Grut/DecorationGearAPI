using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearDomain.Data.Entities
{
    public class Provinces
    {
        public int ProvinceId { get; set; }

        public string ProvinceName { get; set; }

        // Khóa ngoại 

        // 1 - n

        public virtual ICollection<CustomerAddresses> CustomerAddresses { get; set; } = new List<CustomerAddresses>();

        public virtual ICollection<Districts> Districts { get; set; } = new List<Districts>();
    }
}
