using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamStoreDatabase
{
    class IceCreamContext: DbContext
    {
        public DbSet<IceCreamStore> IceCreamStores { get; set; }
        public DbSet<IceCreamMaker> IceCreamMakers { get; set; }
        public DbSet<IceCream> IceCreamFlavors { get; set; }
        public DbSet<Seller> IceCreamSellers { get; set; }
        public DbSet<Selling> IceCreamSelling { get; set; }
    }
}
