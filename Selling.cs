using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamStoreDatabase
{
    [Table("IceCreamSelling")]
    class Selling
    {

        [Key]
        public Guid SellingId { get; set; }
          
        public string Address { get; set; }

        [ForeignKey("Address")]
        public IceCreamStore Store { get; set; }

        
        public string Flavor { get; set; }

        [ForeignKey("Flavor")]
        public IceCream IceCreamFlavour { get; set; }

        public override string ToString()
        {
            return SellingId + ";" + Address + ";" + Flavor;
        }
    }
}
