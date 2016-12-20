using IceCreamStoreDatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IceCreamStoreDatabase
{
    public partial class IceCreamDatabaseForm : Form
    {
        public IceCreamDatabaseForm()
        {
            InitializeComponent(); 
            DatabaseWorker.updateStoreListBox(StoreListBox);
            DatabaseWorker.updateMakersListBox(makersListBox);
            DatabaseWorker.updateIceCreamListBox(IceCreamListBox);
            DatabaseWorker.updateSellersListBox(sellersListBox);
            DatabaseWorker.updateSellingListBox(sellingListBox);
        }

        private void storeInsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new IceCreamContext())
                {
                    string address = AddressTextBox.Text;
                    string workingHours = workingHoursTextBox.Text;
                    IceCreamStore store = new IceCreamStore { Address = address, WorkingHours = workingHours };
                    db.IceCreamStores.Add(store);
                    db.SaveChanges();
                    ////StoreListBox.Items.Add(store);
                }
            }
            catch (Exception ex)
            {
                storeErrorLabel.Text = ex.Message;
            }
        }

        private void StoreDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string keyText = DatabaseWorker.getWordFromListBox(StoreListBox, 0);
                using (var db = new IceCreamContext())
                {
                    var deleteStore = (from store in db.IceCreamStores
                                       where store.Address == keyText
                                       select store).First();

                    db.IceCreamStores.Remove(deleteStore);
                    db.SaveChanges();
                    StoreListBox.Items.Remove(StoreListBox.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                storeErrorLabel.Text = ex.Message;
            }
        }

        private void StoreUpdateButton_Click(object sender, EventArgs e)
        {

            try
            {
                string keyText = DatabaseWorker.getWordFromListBox(StoreListBox, 0);

                using (var db = new IceCreamContext())
                {
                    var updateStore = (from store in db.IceCreamStores
                                       where store.Address == keyText
                                       select store).First();

                    string updateInfo = "update IceCreamStores set Adress='" + AddressTextBox.Text + "',WorkingHours='" + workingHoursTextBox.Text 
                        + "' where Adress='" + updateStore.Address + "';";
                    DatabaseWorker.ConnectionSQL(updateInfo);
                    DatabaseWorker.updateStoreListBox(StoreListBox);

                }

            }
            catch (Exception ex)
            {
                storeErrorLabel.Text = ex.Message;
            }
        }

        private void insertIceCreamButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new IceCreamContext())
                {
                    string flavor = flavorTextBox.Text;
                    double price = Convert.ToDouble(pricePerKiloTextBox.Text);
                    string makerId = MakerTextBox.Text;

                    var maker = (from iceCreamMaker in db.IceCreamMakers
                                 where iceCreamMaker.MakersPersonalId == makerId
                                 select iceCreamMaker).First();

                    IceCream iceCream = new IceCream { Flavor = flavor, PricePerKilo = price, Maker = maker};
                    db.IceCreamFlavors.Add(iceCream);
                    db.SaveChanges();
                    IceCreamListBox.Items.Add(iceCream.ToString());
                }
            }
            catch (Exception ex)
            {
                iceCreamErrorLabel.Text = ex.Message;
            }
        }

        private void deleteIceCreamButton_Click(object sender, EventArgs e)
        {
            try
            {
                string keyText = DatabaseWorker.getWordFromListBox(IceCreamListBox, 0);

                using (var db = new IceCreamContext())
                {
                    var deleteIceCream = (from iceCream in db.IceCreamFlavors
                                          where iceCream.Flavor == keyText
                                          select iceCream).First();

                    db.IceCreamFlavors.Remove(deleteIceCream);
                    db.SaveChanges();
                    IceCreamListBox.Items.Remove(IceCreamListBox.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                iceCreamErrorLabel.Text = ex.Message;
            }
        }

        private void updateIceCreamButton_Click(object sender, EventArgs e)
        {
            try
            {
                string keyText = DatabaseWorker.getWordFromListBox(IceCreamListBox, 0);
                string makerId = DatabaseWorker.getWordFromListBox(IceCreamListBox, 2);

                using (var db = new IceCreamContext())
                {
                    var updateIceCream = (from iceCream in db.IceCreamFlavors
                                          where iceCream.Flavor == keyText
                                          select iceCream).First();

                    var iceCreamMaker = (from maker in db.IceCreamMakers
                                         where maker.MakersPersonalId == makerId
                                         select maker).First();

                    string updateInfo = "update IceCreamFlavors set Flavor='" + flavorTextBox.Text + "',PricePerKilo='" + pricePerKiloTextBox.Text 
                        + "',IceCreamMaker='" + iceCreamMaker + "' where Flavor='" + updateIceCream.Flavor + "';";
                    DatabaseWorker.ConnectionSQL(updateInfo);
                    DatabaseWorker.updateIceCreamListBox(IceCreamListBox);
                }
            }
            catch (Exception ex)
            {
                iceCreamErrorLabel.Text = ex.Message;
            }
        }

        private void makerInsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new IceCreamContext())
                {
                    string id = makerIdTextBox.Text;
                    string firstName = makerFirstNameTextBox.Text;
                    string lastName = makerLastNameTextBox.Text;
                    float salary  = (float)Convert.ToDouble(MakerSalaryTextBox.Text);
                    string number = MakerPhoneNumberTextBox.Text;

                    IceCreamMaker iceCreamMaker = new IceCreamMaker { MakersPersonalId = id, FirstName = firstName, LastName = lastName, Salary = salary,
                        PhoneNumber = number};
                    db.IceCreamMakers.Add(iceCreamMaker);
                    db.SaveChanges();
                    makersListBox.Items.Add(iceCreamMaker.ToString());
                }
            }
            catch (Exception ex)
            {
                iceCreamMakerErrorLabel.Text = ex.Message;
            }
        }

        private void makerDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string keyText = DatabaseWorker.getWordFromListBox(makersListBox, 0);

                using (var db = new IceCreamContext())
                {
                    var deleteMaker = (from maker in db.IceCreamMakers
                                          where maker.MakersPersonalId == keyText
                                          select maker).First();

                    db.IceCreamMakers.Remove(deleteMaker);
                    db.SaveChanges();
                    makersListBox.Items.Remove(makersListBox.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                makerErrorLabel.Text = ex.Message;
            }
        }

        private void makerUpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                string keyText = DatabaseWorker.getWordFromListBox(makersListBox, 0);
                using (var db = new IceCreamContext())
                {
                    var updateMaker = (from maker in db.IceCreamMakers
                                          where maker.MakersPersonalId == keyText
                                          select maker).First();


                    string updateInfo = "update IceCreamMakers set MakersPersonalID='" + makerIdTextBox.Text + "',FirstName='" 
                        + makerFirstNameTextBox.Text + "',LastName='" + makerLastNameTextBox.Text + "',Salary='" + MakerSalaryTextBox.Text 
                        + "',PhoneNumber='" + MakerPhoneNumberTextBox.Text + "' where makersPersonalID='" + updateMaker.MakersPersonalId + "';";
                    DatabaseWorker.ConnectionSQL(updateInfo);
                    DatabaseWorker.updateMakersListBox(makersListBox);
                }
            }
            catch (Exception ex)
            {
                makerErrorLabel.Text = ex.Message;
            }
        }

        private void sellerInsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new IceCreamContext())
                {
                    string id = sellerPersonalIdTextBox.Text;
                    string firstName = sellerFirstNameTextBox.Text;
                    string lastName = sellerLastNameTextBox.Text;
                    float salary = (float)Convert.ToDouble(sellerSalaryTextBox.Text);
                    string work = sellerWorkPlaceTextBox.Text;

                    var store = (from iceCreamStore in db.IceCreamStores
                                 where iceCreamStore.Address == work
                                 select iceCreamStore).First();

                    Seller seller = new Seller
                    {
                        PersonalId = id,
                        FirstName = firstName,
                        LastName = lastName,
                        Salary = salary,
                        Store = store
                    };
                    db.IceCreamSellers.Add(seller);
                    db.SaveChanges();
                    sellersListBox.Items.Add(seller.ToString());
                }
            }
            catch (Exception ex)
            {
                sellerErrorLabel.Text = ex.Message;
            }
        }

        private void sellerDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string keyText = DatabaseWorker.getWordFromListBox(sellersListBox, 0);

                using (var db = new IceCreamContext())
                {
                    var deleteSeller = (from seller in db.IceCreamSellers
                                       where seller.PersonalId == keyText
                                       select seller).First();

                    db.IceCreamSellers.Remove(deleteSeller);
                    db.SaveChanges();
                    sellersListBox.Items.Remove(sellersListBox.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                sellerErrorLabel.Text = ex.Message;
            }
        }

        private void sellerUpdateNumber_Click(object sender, EventArgs e)
        {
            try
            {
                string keyText = DatabaseWorker.getWordFromListBox(sellersListBox, 0);
                string work = DatabaseWorker.getWordFromListBox(sellersListBox, 4);
                using (var db = new IceCreamContext())
                {
                    var updateSeller = (from seller in db.IceCreamSellers
                                       where seller.PersonalId == keyText
                                       select seller).First();

                    var iceCreamStore = (from store in db.IceCreamStores
                                         where store.Address == work
                                         select store).First();

                    string updateInfo = "update IceCreamSellers set PersonalId='" + sellerPersonalIdTextBox.Text + "',FirstName='"
                        + sellerFirstNameTextBox.Text + "',LastName='" + sellerLastNameTextBox.Text + "',Salary='" + sellerSalaryTextBox.Text
                        + "',IceCreamStore='" + iceCreamStore + "' where Address='" + updateSeller.PersonalId + "';";
                    DatabaseWorker.ConnectionSQL(updateInfo);
                    DatabaseWorker.updateSellersListBox(sellersListBox);
                }
            }
            catch (Exception ex)
            {
                sellerErrorLabel.Text = ex.Message;
            }
        }

        private void sellingInsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new IceCreamContext())
                {
                    string storeAddress = iceCreamStoreTextBox.Text;
                    string flavor = iceCreamTextBox.Text;

                    var store = (from iceCreamStore in db.IceCreamStores
                                 where iceCreamStore.Address == storeAddress
                                 select iceCreamStore).First();

                    var iceCreamFlavor = (from iceCream in db.IceCreamFlavors
                                          where iceCream.Flavor == flavor
                                          select iceCream).First();

                    Selling selling = new Selling
                    {
                        SellingId = Guid.NewGuid(),
                        Store = store,
                        IceCreamFlavour = iceCreamFlavor
                    };
                    db.IceCreamSelling.Add(selling);
                    db.SaveChanges();
                    sellingListBox.Items.Add(selling.ToString());
                }
            }
            catch (Exception ex)
            {
                sellingErrorLabel.Text = ex.Message;
            }
        }

        private void sellingDeleteButton_Click(object sender, EventArgs e)
        {

            try
            {
                var id = new Guid(DatabaseWorker.getWordFromListBox(sellingListBox, 0));

                using (var db = new IceCreamContext())
                {
                    var deleteSelling = (from selling in db.IceCreamSelling
                                         where selling.SellingId == id
                                        select selling).First();

                    db.IceCreamSelling.Remove(deleteSelling);
                    db.SaveChanges();
                    sellingListBox.Items.Remove(sellingListBox.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                sellingErrorLabel.Text = ex.Message;
            }
        }

        private void sellingUpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var id = new Guid(DatabaseWorker.getWordFromListBox(sellingListBox, 0));
                string address = DatabaseWorker.getWordFromListBox(sellingListBox, 1);
                string flavor = DatabaseWorker.getWordFromListBox(sellingListBox, 2);
                using (var db = new IceCreamContext())
                {
                    var updateSelling = (from selling in db.IceCreamSelling
                                        where selling.SellingId == id
                                        select selling).First();

                    var newStore = (from store in db.IceCreamStores
                                    where store.Address == address
                                    select store).First();

                    var newIceCream = (from iceCream in db.IceCreamFlavors
                                    where iceCream.Flavor == flavor
                                    select iceCream).First();

                    string updateInfo = "update IceCreamSelling set Store='" + newStore + "',IceCreamFlavor='"
                        + newIceCream + "' where SellingId='" + updateSelling.SellingId + "';";
                    DatabaseWorker.ConnectionSQL(updateInfo);
                    DatabaseWorker.updateSellingListBox(sellingListBox);
                }
            }
            catch (Exception ex)
            {
                sellingErrorLabel.Text = ex.Message;
            }
        }

        private void workPlaceButton_Click(object sender, EventArgs e)
        {
            DatabaseWorker.ShowSellersWorks(worksListBox);
        }

        private void iceCreambyMakersButton_Click(object sender, EventArgs e)
        {
            worksListBox.Items.Clear();
            using (var db = new IceCreamContext())
            {
                var queryIceCreamByMakers =
                    from iceCream in db.IceCreamFlavors
                    group iceCream by iceCream.MakersPersonalId into newGroup
                    orderby newGroup.Key
                    select newGroup;

                foreach (var makerId in queryIceCreamByMakers)
                {
                    foreach(var iceCream in makerId)
                    {
                        worksListBox.Items.Add(makerId.Key + ", " + iceCream.Flavor);
                    }
                }
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            worksListBox.Items.Clear();
            using (var db = new IceCreamContext())
            {
                var iceCreamPrice = (from iceCream in db.IceCreamFlavors
                                    select iceCream.PricePerKilo).ToArray();


                double sum = iceCreamPrice.Aggregate((a, b) => b + a);
                int count = iceCreamPrice.Count();
                worksListBox.Items.Add(sum/count);

            }
        }
    }
}
