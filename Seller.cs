using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamStoreDatabase
{
    [Table("IceCreamSellers")]
    class Seller
    {
        [Key]
        public string PersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Salary { get; set; }

        
        public string Address { get; set; }

        [ForeignKey("Address")]
        public IceCreamStore Store { get; set; }

        public override string ToString()
        {
            return PersonalId + ";" + FirstName + ";" + LastName + ";" + Salary + ";" + Address;
        }
    }
}
