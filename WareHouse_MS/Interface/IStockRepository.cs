using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouse_MS.Domain;

namespace WareHouse_MS.Interface
{
    public interface IStockRepository
    {
        string SellProduct(int productId, int cnt);
        string BuyProduct(Stock productInStock);
        List<Stock> GetSellProductList();
        string BuyProduct(Stock productInStock, int Barcode);

    }
}
