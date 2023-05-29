using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse_MS.Domain
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Barcode { get; set; }
    }
}
