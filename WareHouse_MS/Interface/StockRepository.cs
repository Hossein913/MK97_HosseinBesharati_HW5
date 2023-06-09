﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

            List<Stock> Stocks = GetSellProductList();

            if (Stocks == null)
            {
                Stocks = new List<Stock>();
            }

            if (!Stocks.Contains(productInStock))
            {
                Stocks.Add(productInStock);
                JsonConvert.SerializeObject(Stocks).WriteOnFile("\\DataBase\\StockJson.json");
            }
            else
            {
                var stock = Stocks.Single<Stock>(item => item.StockId == productInStock.StockId);

                stock.Name = productInStock.Name;
                stock.ProductPrice = UpdatePrice(stock, productInStock);
                stock.ProductQuantity += productInStock.ProductQuantity;
                JsonConvert.SerializeObject(Stocks).WriteOnFile("\\DataBase\\StockJson.json");
            }

            return productInStock.Name;

        }
        public string BuyProduct(Stock productInStock, int Barcode)
        {
            Product product =
            new Product() { ProductId = productInStock.ProductId, Name = productInStock.Name, Barcode = Barcode };
            productRepository.AddProduct(product);
            return BuyProduct(productInStock);
        }
        public string SellProduct(int productId, int count)
        {
            List<Stock> Stocks = GetSellProductList();
            var stock = Stocks.Single<Stock>(item => item.ProductId == productId);
            if (GetProductQuantity(productId) >= count)
            {
                stock.ProductQuantity -= count;
                JsonConvert.SerializeObject(Stocks).WriteOnFile("\\DataBase\\StockJson.json");
            }
            else
            {
                throw new NotEnoughQuantity($"There isn't enough quantity form {GetProductById(productId)} Product");
            }
            return stock.Name;

        }

        public List<Stock> GetSellProductList()
        {
            CheckDatabase(Common.GetProjectDirectory("\\DataBase\\StockJson.json"));
            string DatabaseString = File.ReadAllText(Common.GetProjectDirectory("\\DataBase\\StockJson.json"));

            if (DatabaseString != "")
            {
                List<Stock> stock = JsonConvert.DeserializeObject<List<Stock>>(DatabaseString);
                return stock;
            }
            else
            {
                return null;
            }


        }

        public Stock GetProductById(int id)
        {
            string DatabaseString = File.ReadAllText(Common.GetProjectDirectory("\\DataBase\\StockJson.json"));
            Stock products = JsonConvert.DeserializeObject<List<Stock>>(DatabaseString).SingleOrDefault(item => item.ProductId == id);
            return products;

        }

        private int GetProductQuantity(int ProductId)
        {

            string DatabaseString = File.ReadAllText(Common.GetProjectDirectory("\\DataBase\\StockJson.json"));
            Stock products = JsonConvert.DeserializeObject<List<Stock>>(DatabaseString).SingleOrDefault(item => item.ProductId == ProductId);

            if (products == null)
            {
                throw new ProductNotFound("There isn't any Product in WareHouse");
            }

            return products.ProductQuantity;


        }
        private void CheckDatabase(string Directory)
        {
            if (!File.Exists(Directory))
            {
                File.Create(Directory);
            }
        }
        private decimal UpdatePrice(Stock stock, Stock productInStock)
        {
            return ((stock.ProductPrice * stock.ProductQuantity) +
                (productInStock.ProductPrice * productInStock.ProductQuantity)) /
                (stock.ProductQuantity + productInStock.ProductQuantity);
        }

    }
}
