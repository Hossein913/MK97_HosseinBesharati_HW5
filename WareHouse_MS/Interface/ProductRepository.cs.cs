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

            List<Product> products = GetProductList();
            if (products == null)
            {
                products = new List<Product>();
            }

            products.Add(product);
            SerializedString = JsonConvert.SerializeObject(products);

            using (StreamWriter writer = new StreamWriter(Common.GetProjectDirectory("\\DataBase\\ProductJson.json")))
                writer.Write(SerializedString);

            return product.Name;
        }

        public string GetProductById(int id)
        {
            string DatabaseString = File.ReadAllText(Common.GetProjectDirectory("\\DataBase\\ProductJson.json"));
            string productsName = String.Empty;
            Product products = null;
            if (DatabaseString != "")
            {
                products = JsonConvert.DeserializeObject<List<Product>>(DatabaseString).SingleOrDefault(item => item.ProductId == id);
                if (products != null)
                {
                    productsName = products.Name;
                }
            }

            return productsName;

        }

        public string GetProductByName(string name)
        {
            string DatabaseString = File.ReadAllText(Common.GetProjectDirectory("\\DataBase\\ProductJson.json"));
            string productsName = String.Empty;
            Product products = null;
            if (DatabaseString != "")
            {
                products = JsonConvert.DeserializeObject<List<Product>>(DatabaseString).SingleOrDefault(item => item.Name == name);
                if (products != null)
                {
                    productsName = products.Name;
                }
            }

            return productsName;

        }
        public List<Product> GetProductList()
        {
            CheckDatabase(Common.GetProjectDirectory("\\DataBase\\ProductJson.json"));
            string DatabaseString = File.ReadAllText(Common.GetProjectDirectory("\\DataBase\\ProductJson.json"));
            var products = JsonConvert.DeserializeObject<List<Product>>(DatabaseString);
            return products;
        }

        public int GetlastProductId()
        {
            string DatabaseString = File.ReadAllText(Common.GetProjectDirectory("\\DataBase\\ProductJson.json"));

            if (DatabaseString != "")
            {
                return JsonConvert.DeserializeObject<List<Product>>(DatabaseString).Last().ProductId;
            }
            else
            {
                return 0;

            }

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
