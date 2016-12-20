using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamStoreDatabase
{
    [Table("IceCreamMakers")]
    class IceCreamMaker
    {
        [Key]
        public string MakersPersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Salary { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return MakersPersonalId + ";" + FirstName + ";" + LastName + ";" + Salary + ";" + PhoneNumber;
        }
    }
}
