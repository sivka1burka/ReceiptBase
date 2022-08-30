using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category CategoryId { get; set; }
        public int Price { get; set; }

    }
}
