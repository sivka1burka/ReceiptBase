
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.models;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
            using (var ctx = new ReceiptContext())
            {
                //Инициализация начальных данных в базе
                

                #region [x] categoryIninializing
                List<Category> categoriesDefault = new List<Category>();
                Category c1 = new Category()
                {
                    Name = "GPU"
                };
                categoriesDefault.Add(c1);
                Category c2 = new Category()
                {
                    Name = "CPU"

                };
                categoriesDefault.Add(c2);
                Category c3 = new Category()
                {
                    Name = "Motherboard"
                };
                categoriesDefault.Add(c3);
                Category c4 = new Category()
                {
                    Name = "Storage Device"
                };
                categoriesDefault.Add(c4);
                Category c5 = new Category()
                {
                    Name = "Power Supply Unit"
                };
                categoriesDefault.Add(c5);
                ctx.categories.AddRange(categoriesDefault);
                #endregion

                #region [x] cashierInitializing
                List<Cashier> cashierDefault = new List<Cashier>();
                Cashier cashier1 = new Cashier()
                {
                    Name = "Glenn Quagmire"
                };
                cashierDefault.Add(cashier1);
                Cashier cashier2 = new Cashier()
                {
                    Name = "Tony Bamboni"
                };
                cashierDefault.Add(cashier2);
                Cashier cashier3 = new Cashier()
                {
                    Name = "Richard Wiliams"
                };
                cashierDefault.Add(cashier3);
                Cashier cashier4 = new Cashier()
                {
                    Name = "Piter Griffin"
                };
                cashierDefault.Add(cashier4);
                ctx.cashiers.AddRange(cashierDefault);
                #endregion

                #region [x] productInitializing
                List<Product> productsDefault = new List<Product>();
                Product p1 = new Product()
                {
                    Name = "RTX 3090",
                    Price = 2000,
                    CategoryId = c1
                };
                productsDefault.Add(p1);
                Product p2 = new Product()
                {
                    Name = "Intel Core i7 9700K",
                    Price = 1100,
                    CategoryId = c2
                };
                productsDefault.Add(p2);
                Product p3 = new Product()
                {
                    Name = "GTX 1060",
                    Price = 600,
                    CategoryId = c1
                };
                productsDefault.Add(p3);
                Product p4 = new Product()
                {
                    Name = "RTX 2060",
                    Price = 899,
                    CategoryId = c1
                };
                productsDefault.Add(p4);
                Product p5 = new Product()
                {
                    Name = "Gigabyte Z690 Aorus Pro",
                    Price = 3000,
                    CategoryId = c3
                };
                productsDefault.Add(p5);
                Product p6 = new Product()
                {
                    Name = "Corsair CX450",
                    Price = 1500,
                    CategoryId = c5
                };
                productsDefault.Add(p6);
                Product p7 = new Product()
                {
                    Name = "Samsung 980 Pro",
                    Price = 1400,
                    CategoryId = c4
                };
                productsDefault.Add(p7);
                Product p8 = new Product()
                {
                    Name = "Ryzen 5 2600",
                    Price = 700,
                    CategoryId = c2
                };
                productsDefault.Add(p8);
                Product p9 = new Product()
                {
                    Name = "Crucial P5 Plus",
                    Price = 300,
                    CategoryId = c4
                };
                productsDefault.Add(p9);
                ctx.products.AddRange(productsDefault);
                #endregion

                #region [x] receiptsInitializing
                List<Receipt> receiptsDefault = new List<Receipt>
                {
                new Receipt() { PurchaseDate=new DateTime(2017,07,21),CashierId=cashier1,ProductId=p1 },
                new Receipt() { PurchaseDate=new DateTime(2017,03,7),CashierId=cashier2,ProductId=p2 },
                new Receipt() { PurchaseDate=new DateTime(2014,04,14),CashierId=cashier2,ProductId=p3 },
                new Receipt() { PurchaseDate=new DateTime(2015,07,21),CashierId=cashier4,ProductId=p4 },
                new Receipt() { PurchaseDate=new DateTime(2019,09,17),CashierId=cashier3,ProductId=p5 },
                new Receipt() { PurchaseDate=new DateTime(2018,06,14),CashierId=cashier1,ProductId=p6 },
                new Receipt() { PurchaseDate=new DateTime(2021,05,13),CashierId=cashier4,ProductId=p7 },
                new Receipt() { PurchaseDate=new DateTime(2019,04,16),CashierId=cashier3,ProductId=p8 },
                new Receipt() { PurchaseDate=new DateTime(2018,08,27),CashierId=cashier2,ProductId=p4 },
                new Receipt() { PurchaseDate=new DateTime(2021,05,4),CashierId=cashier3,ProductId=p9 },
                new Receipt() { PurchaseDate=new DateTime(2022,01,19),CashierId=cashier1,ProductId=p1 },

                };

                ctx.receipts.AddRange(receiptsDefault);
                ctx.SaveChanges();
                #endregion

            }
            ShowUpdatedTable();
        }
       
        public void ShowUpdatedTable()//Вывод данных из таблицы
        {
            using (var ctx = new ReceiptContext())
            {
                List<Receipt> receipts = new List<Receipt>();
                foreach (var receipt in ctx.receipts)
                {
                    receipts.Add(receipt);
                }
                var res=ctx.receipts.Include("ProductId").Include("CashierId").Include("ProductId.CategoryId").ToList();


                var result = from receipt in res
                             select new { Id = receipt.Id, Purchase_Date = receipt.PurchaseDate, Product_Name = receipt.ProductId.Name, Product_Category = receipt.ProductId.CategoryId.Name, Price = receipt.ProductId.Price, Cashier = receipt.CashierId.Name };

                receiptsGrid.ItemsSource = result;//Заполение панели DockPanel данными из бд
            }
        }
        public void btnReset(object sender,RoutedEventArgs e)//вывод обновленных данных из бд
        {
            ShowUpdatedTable();
        }
        public void btnDelete(object sender,RoutedEventArgs e)//удаление строки из бд
        {
            ShowBox.Text = "";
            if (TB6.Text.Length > 0)
            {
                int idDelete = Int32.Parse(TB6.Text);
                using (var ctx = new ReceiptContext())
                {
                    List<Receipt> receipts = new List<Receipt>();
                    Receipt DeleteReceipt = ctx.receipts.Where(x => x.Id == idDelete).Include("ProductId").Include("CashierId").Include("ProductId.CategoryId").FirstOrDefault();
                    //Category DeleteCategory = ctx.categories.Where(x => x.Id == DeleteReceipt.ProductId.CategoryId.Id).FirstOrDefault();
                    //Cashier DeleteCashier = ctx.cashiers.Where(x => x.Id == DeleteReceipt.CashierId.Id).FirstOrDefault();
                    //Product DeleteProduct = ctx.products.Where(x => x.Id == DeleteReceipt.ProductId.Id).FirstOrDefault();
                    if (DeleteReceipt != null)//Проверка на null значение
                    {
                        //ctx.categories.Remove(DeleteCategory);
                        //ctx.products.Remove(DeleteProduct);
                        //ctx.cashiers.Remove(DeleteCashier);
                        ctx.receipts.Remove(DeleteReceipt);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        ShowBox.Text = "Введено неправильное Id записи для удаления!";
                    }

                }
                ShowUpdatedTable();
            }
            else
            {
                ShowBox.Text = ("Поле должно быть заполненным!");
            }
            
            
            
        }
        public void btnFind(object sender,RoutedEventArgs e)
        {
            ShowBox.Text = "";
            if (TB7.Text.Length > 0)
            {
                
                using (var ctx = new ReceiptContext())
                {
                    List<Receipt> receipts = new List<Receipt>();
                    if (rb1.IsChecked == true)
                    {                     
                        receipts = ctx.receipts.Where(x => x.Id == int.Parse(TB7.Text)).Include("ProductId").Include("CashierId").Include("ProductId.CategoryId").ToList();
                    }
                    else if(rb2.IsChecked == true)
                    {
                        receipts = ctx.receipts.Where(x => x.PurchaseDate == DateTime.Parse(TB7.Text)).Include("ProductId").Include("CashierId").Include("ProductId.CategoryId").ToList();
                    }
                    else if (rb3.IsChecked == true)
                    {
                        receipts = ctx.receipts.Where(x => x.ProductId.Name == TB7.Text).Include("ProductId").Include("CashierId").Include("ProductId.CategoryId").ToList();
                    }
                    else if (rb4.IsChecked == true)
                    {
                        receipts = ctx.receipts.Where(x => x.ProductId.CategoryId.Name == TB7.Text).Include("ProductId").Include("CashierId").Include("ProductId.CategoryId").ToList();
                    }
                    else if (rb5.IsChecked == true)
                    {
                        receipts = ctx.receipts.Where(x => x.ProductId.Price == int.Parse(TB7.Text)).Include("ProductId").Include("CashierId").Include("ProductId.CategoryId").ToList();
                    }
                    else if (rb6.IsChecked == true)
                    {
                        receipts = ctx.receipts.Where(x => x.CashierId.Name == TB7.Text).Include("ProductId").Include("CashierId").Include("ProductId.CategoryId").ToList();
                    }

                    if (receipts != null)//Проверка на null значение
                    {
                        var result = from receipt in receipts
                                     select new { Id = receipt.Id, Purchase_Date = receipt.PurchaseDate, Product_Name = receipt.ProductId.Name, Product_Category = receipt.ProductId.CategoryId.Name, Price = receipt.ProductId.Price, Cashier = receipt.CashierId.Name };

                        receiptsGrid.ItemsSource = result;
                    }
                    else
                    {
                        ShowBox.Text = "Записи не существует!";
                    }

                }
            }
            else
            {
                ShowBox.Text=("Поле должно быть заполненным!");
            }
            
            

        }
        public void btnAdd(object sender,RoutedEventArgs e)
        {
            ShowBox.Text = "";
  
            if (TB3.Text.Length>0 && TB4.Text.Length > 0 && TB1.Text.Length > 0 && TB2.Text.Length > 0 && TB5.Text.Length > 0) //Проверка на null значения
            {
                string CategoryName = TB3.Text;
                string CashierName = TB5.Text;
                string ProductName = TB2.Text;
                int ProductPrice = Int32.Parse(TB4.Text);
                DateTime ReceiptPurchaseDate = DateTime.Parse(TB1.Text);

                using (var ctx = new ReceiptContext())
                {
                    Cashier cashier1 = new Cashier()
                    {
                        Name = CashierName
                    };

                    Category category1 = new Category()
                    {
                        Name = CategoryName
                    };

                    Product product1 = new Product()
                    {
                        Name = ProductName,
                        Price = ProductPrice,
                        CategoryId = category1
                    };

                    Receipt receipt1 = new Receipt() { PurchaseDate = ReceiptPurchaseDate, CashierId = cashier1, ProductId = product1 };
                    ctx.receipts.Add(receipt1);
                    ctx.SaveChanges();
                }
                ShowUpdatedTable();

            }
            else
            {
                ShowBox.Text = "Поля должны быть заполненными для добавления записи!";
            }
           
        }
        private void btnEdit(object sender, RoutedEventArgs e)
        {
            if (TB3.Text.Length > 0 && TB4.Text.Length > 0 && TB1.Text.Length > 0 && TB2.Text.Length > 0 && TB5.Text.Length > 0) //Проверка на null значения
            {
                using (var ctx = new ReceiptContext())
                {
                    int ReceiptId = receiptsGrid.SelectedIndex;
                    if (ReceiptId < 0)
                    {
                        ShowBox.Text = "Выберите запись!";
                    }
                    else
                    {
                        Receipt receipt = ctx.receipts.
                            Where(x => x.Id == ReceiptId + 1).Include("ProductId").Include("CashierId").Include("ProductId.CategoryId").FirstOrDefault();
                        receipt.PurchaseDate = DateTime.Parse(TB1.Text);
                        receipt.ProductId.Name = TB2.Text;
                        receipt.ProductId.CategoryId.Name = TB3.Text;
                        receipt.ProductId.Price = int.Parse(TB4.Text);
                        receipt.CashierId.Name = TB5.Text;


                        ctx.Entry(receipt).State = EntityState.Modified;
                        ctx.SaveChanges();
                        ShowUpdatedTable();
                    }
                }
            }
            else
            {
                ShowBox.Text = "Поля должны быть заполненными для изменения записи!";
            }

        }


    }
}
