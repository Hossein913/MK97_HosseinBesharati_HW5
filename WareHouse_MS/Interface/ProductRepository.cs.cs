using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouse_MS.Domain;

namespace WareHouse_MS.Interface
{
    public class ProductRepository : IProductRepository
    {
      
        public string AddProduct(Product product)
        {
            string SerializedString;
            if (CheckProductName(product.Name))
            {

                List<Product> products = GetProductList();
                if (products == null)
                {
                    products = new List<Product>();
                }

                products.Add(product);
                JsonConvert.SerializeObject(products).WriteOnFile(Common.GetProjectDirectory("\\DataBase\\ProductJson.json"));
                return product.Name;

            }

            return null;
        }
        public string GetProductById(int id)
        {
            string DatabaseString = File.ReadAllText(Common.GetProjectDirectory("\\DataBase\\ProductJson.json"));
            string products = String.Empty;
            products = JsonConvert.DeserializeObject<List<Product>>(DatabaseString).Single(item => item.ProductId == id).Name;
            return products;
        }
        public List<Product> GetProductList()
        {
            CheckDatabase(Common.GetProjectDirectory("\\DataBase\\ProductJson.json"));
            string DatabaseString = File.ReadAllText(Common.GetProjectDirectory("\\DataBase\\ProductJson.json"));
            var products = JsonConvert.DeserializeObject<List<Product>>(DatabaseString);
            return products;
        }
        public bool CheckProductName(string productName)
        {
            return true;
        }
        private void CheckDatabase(string Directory)
        {
            if (!File.Exists(Directory))
            {
                File.Create(Directory);
            }
        }

    }
}
