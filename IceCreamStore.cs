using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamStoreDatabase
{
    [Table("IceCreamStores")]
    class IceCreamStore
    {
        [Key]
        public string Address { get; set; }
        public string WorkingHours { get; set; }

        public override string ToString()
        {
            return Address + ";" + WorkingHours;
        }
    }
}
