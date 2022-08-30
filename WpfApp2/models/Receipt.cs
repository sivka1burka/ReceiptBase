using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.models
{
    public class Receipt
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Product ProductId { get; set; }
        public Cashier CashierId { get; set; }



    }
}
