using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.models
{
    public class ReceiptContext : DbContext
    {
        public ReceiptContext() : base("Connection")
        {
            Database.SetInitializer(new ReceiptDBInitializer());
        }
        public DbSet<Receipt> receipts { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Cashier> cashiers { get; set; }

    }
    public class ReceiptDBInitializer :
        DropCreateDatabaseAlways<ReceiptContext>
    {
       
    }
}
