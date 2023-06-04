using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouse_MS.Domain;

namespace WareHouse_MS.Interface
{
    public class StockRepository : IStockRepository
    {

        private IProductRepository productRepository;

        public StockRepository(IProductRepository ProductRepo)
        {
            productRepository = ProductRepo;
        }

        public string BuyProduct(Stock productInStock)
        {
            throw new NotImplementedException();
        }

        public List<Stock> GetSalesProductList()
        {
            throw new NotImplementedException();
        }

        public string SaleProduct(int productId, int cnt)
        {
            throw new NotImplementedException();
        }
    }
}
