using BlApi;
using BlImplementation;
using BO;
using Cart = BO.Cart;
using Order = BO.Order;
using Product = BO.Product;

namespace BlTest
{
    internal class Program
    {
        private static IBl _bl = new Bl();

        static void Main(string[] args)
        {
            BO.Cart cart = new Cart { Items = new List<OrderItem>() };
            BO.Order order = createNewOrder(cart);
            string chosenEntity = entityMenu();

            while (chosenEntity != "0")
            {
                try
                {
                    switch (chosenEntity)
                    {
                        case "1":
                            productTester();
                            break;
                        case "2":
                            orderTestser(order);
                            break;
                        case "3":
                            cartTestser(cart);
                            break;
                        case "0":
                            Console.WriteLine("goodbye");
                            break;
                        default:
                            Console.WriteLine("Choice doesn't exist");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                chosenEntity = entityMenu();
            }
        }

        private static string entityMenu()
        {
            Console.WriteLine($@"

            choose entity performance to check:

            1 - product
            2 - order 
            3 - cart
            0 - exit
            ");
            return Console.ReadLine() ?? "";
        }

        private static string cartTestserMenu()
        {
            Console.WriteLine($@"

            choose performance to check:

            1 - add product to cart
            2 - update amount of product 
            3 - Commit cart
            0 - back
            ");
            return Console.ReadLine() ?? "";
        }

        private static string orderTestserMenu()
        {
            Console.WriteLine(
            $@"choose performance to check:

            1 - get an order
            2 - get all orders
            3 - update shipped date
            4 - update delivery date
            5 - track order
            0 - back");
            return Console.ReadLine() ?? "";
        }

        private static string productTesterMenu()
        {
            Console.WriteLine(
            $@"choose performance to check:

            1 - add product
            2 - delete product
            3 - update product
            4 - get all products
            5 - get product for manager
            6 - get product for customer
            0 - back");
            return Console.ReadLine() ?? "";
        }


        private static BO.Order createNewOrder(BO.Cart cart)
        {
            Order order = new Order()
            {
                CustomerAddress = cart.CustomerAdress,
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                Items = cart.Items,
                DeliveryDate = DateTime.MinValue,
                OrderDate = DateTime.MinValue,
                ShipDate = DateTime.MinValue,
                TotalPrice = 0,
                Status = BO.Enums.Status.confirmed
            };

            return order;
        }

        private static void orderTestser(BO.Order order)
        {
            string choice = orderTestserMenu();
            while (choice != "0")
            {
                switch (choice)
                {
                    case "1":
                        int id = getIdForOrder();
                        Console.WriteLine(_bl.Order.GetOrder(id));
                        break;

                    case "2":
                        IEnumerable<OrderForList?> allProduct = _bl.Order.orderForLists();
                        foreach (OrderForList? item in allProduct)
                            Console.WriteLine(item);
                        break;

                    case "3":
                        id = getIdForOrder();
                        _bl.Order.UpdateshippedDate(id);
                        break;

                    case "4":
                        id = getIdForOrder();
                        _bl.Order.UpdateDeliverdDate(id);
                        break;


                    case "5":
                        id = getIdForOrder();
                        Console.WriteLine(_bl.Order.GetOrderTracking(id).OrderTrackingStatus);
                        break;

                    case "0":
                        Console.WriteLine("return to main menu");
                        break;
                }
                choice = orderTestserMenu();
            }
        }

        private static int getIdForOrder()
        {
            bool flag;
            int id = 0;
            do
            {
                Console.Write("enter Order id: ");
                flag = int.TryParse(Console.ReadLine(), out id);

            } while (!flag);
            return id;
        }

        private static void productTester()
        {
            string choice = productTesterMenu();
            while (choice != "0")
            {
                switch (choice)
                {
                    case "1":
                        Product product = getProduct();
                        _bl.Product.AddProduct(product);
                        break;

                    case "2":
                        int id = getIdForProduct();
                        _bl.Product.DeleteProduct(id);
                        break;

                    case "3":
                        product = getProduct();
                        Console.WriteLine(product);
                        Console.WriteLine();
                        _bl.Product.UpdateProduct(product);
                        break;

                    case "4":
                        Console.WriteLine(printList(_bl.Product.GetProductsForList()));
                        break;

                    case "5":
                        id = getIdForProduct();
                        Console.WriteLine(_bl.Product.GetProduct(id));
                        break;

                    case "6":
                        id = getIdForProduct();
                        Console.WriteLine(_bl.Product.GetProduct(id));
                        break;

                    case "0":
                        Console.WriteLine("return to main menu");
                        break;
                }

                choice = productTesterMenu();
            }
        }

        private static string printList<T>(IEnumerable<T> values)
        => string.Join('\n', values);

        private static int getIdForProduct()
        {
            bool flag;
            int id = 0;
            do
            {
                Console.Write("enter product id: ");
                flag = int.TryParse(Console.ReadLine(), out id);

            } while (!flag);
            return id;
        }

        private static Product getProduct()
        {
            Console.WriteLine(@"Enter id, Name, Category, Price, InStock");
            Product newProduct = new Product
            {
                ID = getIdForProduct(),
                Name = Console.ReadLine()!,
                Categories = (BO.Enums.Category)(Convert.ToInt32(Console.ReadLine())),
                Price = double.Parse(Console.ReadLine()!),
                InStock = int.Parse(Console.ReadLine()!)
            };
            return newProduct;
        }

        private static void cartTestser(BO.Cart cart)
        {
            string choice = cartTestserMenu();

            while (choice != "0")
            {

                switch (choice)
                {
                    case "1":
                        addProductToCart(cart);
                        break;
                    case "2":
                        updateProductInCart(cart);
                        break;
                    case "3":
                        commitCart(cart);
                        break;
                    case "0":
                        Console.WriteLine("return to main menu");
                        break;
                }

                choice = cartTestserMenu();
            }
        }

        private static void commitCart(BO.Cart cart)
        {
            Console.WriteLine("opening new cart, enter info \n");
            Console.WriteLine("Name:");
            cart.CustomerName = Console.ReadLine() ?? "";
            Console.WriteLine("Home address:");
            cart.CustomerAdress = Console.ReadLine() ?? "";
            Console.WriteLine("Email address:");
            cart.CustomerEmail = Console.ReadLine() ?? "";
            cart.TotalPrice = 0;

            _bl.Cart.CommitCart(cart);
        }

        private static void updateProductInCart(BO.Cart cart)
        {
            int amount;
            bool flag;
            int id = getIdForProduct();

            do
            {
                Console.Write("new amount of product");
                flag = int.TryParse(Console.ReadLine(), out amount);

            } while (!flag);
            Console.WriteLine(_bl.Cart.UpdateOrderItem(cart, id, amount));
        }

        private static void addProductToCart(BO.Cart cart)
        {
            int id = getIdForProduct();
            Console.WriteLine(_bl.Cart.AddOrderItem(cart, id));
        }
    }
}



