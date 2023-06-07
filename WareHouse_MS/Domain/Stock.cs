﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse_MS.Domain
{
    public class Stock
    {
        public Guid StockId { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductPirce { get; set; }
        public override string ToString()
        {
            return $"{ProductId}  {Name}   {ProductPirce}   {ProductQuantity}";
        }
    }
}
