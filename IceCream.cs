using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamStoreDatabase
{
    [Table("IceCreamFlavors")]
    class IceCream
    {
        [Key]
        public string Flavor { get; set; }
        public double PricePerKilo { get; set; }

        
        public string MakersPersonalId { get; set; }

        [ForeignKey("MakersPersonalId")]
        public IceCreamMaker Maker { get; set; }

        public override string ToString()
        {
            return Flavor + ";" + PricePerKilo + ";" + MakersPersonalId;
        }
    }
}
