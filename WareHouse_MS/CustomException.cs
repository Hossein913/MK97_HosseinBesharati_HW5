using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse_MS
{
    public class ProductNotFound : SystemException
    {
        public ProductNotFound(string msg) : base(msg) { }
        public ProductNotFound() : base() { }

    }

    public class NotEnoughQuantity : SystemException
    {
        public NotEnoughQuantity(string msg) : base(msg) { }

    }
}
