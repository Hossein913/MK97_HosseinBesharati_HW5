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
        private static string workingDirectory = Environment.CurrentDirectory;
        private static string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        private static string Database = String.Concat(projectDirectory, "\\Database\\ProductJson.json");
        

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
                SerializedString = JsonConvert.SerializeObject(products);

                using (StreamWriter writer = new StreamWriter(Database))
                    writer.Write(SerializedString);

                return product.Name;


            }

            return null;
        }

        public string GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductList()
        {
            CheckDatabase(Database);
            string DatabaseString = File.ReadAllText(Database);
            var products = JsonConvert.DeserializeObject<List<Product>>(DatabaseString);
            return products;
        }
        public bool CheckProductName(string productName)
        {
            throw new NotImplementedException();
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
