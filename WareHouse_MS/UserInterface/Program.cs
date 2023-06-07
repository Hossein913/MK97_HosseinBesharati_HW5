using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WareHouse_MS.Domain;
using WareHouse_MS.Interface;
using WareHouse_MS.UserInterface;

namespace WareHouse_MS
{
    class Program
    {
        public static IProductRepository productRepository;
        public static IStockRepository stockRepository;
        static Program()
        {
            productRepository = new ProductRepository();
            stockRepository = new StockRepository(productRepository);
        }

        static void Main(string[] args)
        {
            try
            {
                EntryPoint();
            }
            catch (Exception msg)
            {

                Console.WriteLine(msg.Message);
                Console.ReadLine();
            }
        }


        private static void EntryPoint()
        {
            bool menuFlag;
            string menuInput = "";

            Console.Clear();
            Console.WriteLine(".: Wellcome to WareHouse Managment system :.");
            Console.WriteLine();
            Console.WriteLine(" -> Main Menu ");
            Console.WriteLine("[1]. Buy");
            Console.WriteLine("[2]. Sell");
            Console.WriteLine("[3]. List");
            Console.WriteLine();

            do
            {
                Console.Write("Insert menu number:");
                menuInput = Console.ReadLine();
                try
                {
                    menuFlag = Validation.MenuInputValidation(menuInput, 4);
                }
                catch (SystemException ex)
                {
                    Console.WriteLine(ex.Message);
                    menuFlag = true;
                }

            } while (menuFlag);

            switch (menuInput)
            {
                case "1":
                    Buy();
                    break;
                case "2":
                    Sell();
                    break;
                case "3":
                    GetList();
                    break;
                default:
                    EntryPoint();
                    break;


            }

        }

        private static void GetList()
        {
            int Printcounter = 1;
            string Input;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(@"[Id]   [Name]   [pirce]     [quantity]");
            Console.WriteLine("_____________________________________________");

            try
            {
                var ProductList = stockRepository.GetSellProductList();
                if (ProductList == null)
                {
                    Console.WriteLine("There isn't any product in WareHouse");
                }
                else
                {
                    ProductList.ForEach(item =>
                    {
                        if (Printcounter % 2 == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        Console.WriteLine(item.ToString());
                        Console.ResetColor();
                        Printcounter++;
                    });

                }

            }
            catch (Exception msg)
            {

                Console.WriteLine(msg.Message);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("to Go previous menu write Q");
                Console.ResetColor();
                Input = Console.ReadLine();
                EntryPoint();
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("to Go previous menu write Q");
            Console.ResetColor();
            Input = Console.ReadLine();
            EntryPoint();


        }

        private static void Sell()
        {
            string productId;
            string quantity;
            string ProductName = "";
            PageHeader();


            Console.WriteLine("Insert Product:");

            Console.Write("  Product Id: ");
            productId = Console.ReadLine();
            BackMenu(productId);

            Console.Write("Product quantity: ");
            quantity = Console.ReadLine();
            BackMenu(quantity);

            try
            {
                ProductName = stockRepository.SellProduct(int.Parse(productId), int.Parse(quantity));
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
                Thread.Sleep(4000);
                Sell();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{quantity} {ProductName} had sold.");
            Console.ResetColor();
            Thread.Sleep(2000);
            EntryPoint();

        }

        private static void Buy()
        {

            const int idStartPoint = 1000;
            int Id = 0;
            string ProductName = "";
            string name;
            string pirce;
            string quantity;
            string barcode;

            bool ProductNameValidation = false;

            if (productRepository.GetlastProductId() == 0)
            {
                Id = idStartPoint + productRepository.GetlastProductId() + 1;
            }
            else
            {
                Id = productRepository.GetlastProductId() + 1;
            }

            PageHeader();

            do
            {

                Console.WriteLine("Insert Product:");

                Console.Write("Name[Cptal(1),Lower(3),_,Number(3)]: ");
                name = Console.ReadLine();
                BackMenu(name);

                Console.Write(" Product pirce: ");
                pirce = Console.ReadLine();
                BackMenu(pirce);


                Console.Write("Product quantity: ");
                quantity = Console.ReadLine();
                BackMenu(quantity);


                ProductNameValidation = Validation.CheckProductName(name);

                if (!ProductNameValidation)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid product name");
                    Thread.Sleep(2000);
                }


            } while (!ProductNameValidation);

            Stock stock = new Stock
            {
                StockId = Guid.NewGuid(),
                Name = name,
                ProductId = Id,
                ProductPrice = int.Parse(pirce),
                ProductQuantity = int.Parse(quantity)
            };

            try
            {

                if (productRepository.GetProductByName(stock.Name) != String.Empty)
                {
                    stockRepository.BuyProduct(stock);
                }
                else
                {
                    Console.WriteLine("No product with this name has been registered yet, to register insert product Barcood");
                    Console.Write("  Product Barcode: ");
                    barcode = Console.ReadLine();
                    BackMenu(barcode);
                    ProductName = stockRepository.BuyProduct(stock, int.Parse(barcode));
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{ProductName} inserted into wareHouse completely");
                Console.ResetColor();
                Thread.Sleep(3000);
                EntryPoint();
            }
            catch (System.Exception msg)
            {

                Console.WriteLine(msg.Message);
                Thread.Sleep(3000);
                EntryPoint();
            }



        }

        private static void BackMenu(string Text)
        {
            if (Text == "Q")
            {
                EntryPoint();
            }
        }

        private static void PageHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("to Go previous menu write Q");
            Console.ResetColor();
        }
    }
}
