using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouse_MS.Domain;

namespace WareHouse_MS.Interface
{
    public interface IProductRepository
    {
        string AddProduct(Product product);
        List<Product> GetProductList();
        string GetProductById(int id);
    }
}
