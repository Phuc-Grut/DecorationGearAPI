using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearDomain.Data.Entities
{
    public class CustomerAddresses
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        public string AddressDetail { get; set; }

        public string Note { get; set; }

        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        public int WardId { get; set; }

        public int UserID { get; set; }


        // Khóa ngoại 

        // n - 1

        public virtual User User { get; set; }

        public virtual Provinces Provinces { get; set; }

        public virtual Districts Districts { get; set; }

        public virtual Wards Wards { get; set; }
    }
}
