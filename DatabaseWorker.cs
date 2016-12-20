using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IceCreamStoreDatabase
{
    static class DatabaseWorker
    {

        public static string connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = IceCreamStoreDatabase.IceCreamContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
        public static void ConnectionSQL(string updateInfo)
        {
  
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(updateInfo, connection);
            SqlDataReader dataReader;
            connection.Open();
            dataReader = command.ExecuteReader();
            connection.Close();
        }

        public static void updateStoreListBox(ListBox StoreListBox)
        {
            StoreListBox.Items.Clear();
            using (var db = new IceCreamContext())
            {
                var query = from b in db.IceCreamStores
                            orderby b.Address
                            select b;
                foreach (var item in query)
                {
                    StoreListBox.Items.Add(item.ToString());
                }
            }
        }
        public static void updateMakersListBox(ListBox makersListBox)
        {
            makersListBox.Items.Clear();
            using (var db = new IceCreamContext())
            {
                var query = from b in db.IceCreamMakers
                            orderby b.MakersPersonalId
                            select b;
                foreach (var item in query)
                {
                    makersListBox.Items.Add(item.ToString());
                }

            }
        }

        public static void updateIceCreamListBox(ListBox IceCreamListBox)
        {
            IceCreamListBox.Items.Clear();
            using (var db = new IceCreamContext())
            {
                var iceCreamList = from iceCream in db.IceCreamFlavors
                            orderby iceCream.Flavor
                            select iceCream;

                foreach (var iceCream in iceCreamList)
                {
                    IceCreamListBox.Items.Add(iceCream.ToString());
                }
            }
        }

        public static void updateSellersListBox(ListBox sellersListBox)
        {
            sellersListBox.Items.Clear();
            using (var db = new IceCreamContext())
            {
                var sellersList = from seller in db.IceCreamSellers
                                   orderby seller.PersonalId
                                   select seller;

                foreach (var seller in sellersList)
                {
                    sellersListBox.Items.Add(seller.ToString());
                }
            }
        }

        public static void updateSellingListBox(ListBox sellingListBox)
        {
            sellingListBox.Items.Clear();
            using (var db = new IceCreamContext())
            {
                var sellingList = from selling in db.IceCreamSelling
                                   orderby selling.SellingId
                                   select selling;

                foreach (var selling in sellingList)
                {
                    sellingListBox.Items.Add(selling.ToString());
                }
            }
        }


        public static string getWordFromListBox(ListBox box, int index)
        {
            string item = box.GetItemText(box.SelectedItem);
            string keyText = item.Split(';')[index];
            return keyText;
        }

        public static void ShowSellersWorks(ListBox worksListBox)
        {
            worksListBox.Items.Clear();
            using (var db = new IceCreamContext())
            {
                var sellersWorks = from seller in db.IceCreamSellers
                                    join store in db.IceCreamStores on seller.Address equals store.Address
                                    select new { FirstName = seller.FirstName,
                                                 LastName = seller.LastName,
                                                 Work = seller.Address,
                                                 Time = store.WorkingHours};

                foreach(var sellerWork in sellersWorks)
                {
                    worksListBox.Items.Add(sellerWork.FirstName + ", " + sellerWork.LastName + ", " + sellerWork.Work + ", " + sellerWork.Time);
                }
            }
            
        }
    }
}
