using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

namespace Dal;

/// <summary>
/// 
/// </summary>
internal static class DataSource
{
    /// <summary>
    /// 
    /// </summary>
    internal static class config
    {
        internal static int NextOrderNumberOrderItem { get => ++LastIDnumberOrderitem; }
        internal static int nextOrderNumber { get => ++OrderItemNumber; }
        internal static int nextProductNumber { get => ++productNumber; }

        private static int OrderItemNumber = 0;
        private static int productNumber = 0;
        private static int LastIDnumberOrderitem = 10000;
    }

    internal static List<Product> Products { get; set; } = new List<Product> { };
    internal static List<Order> orders { get; } = new List<Order> { };
    internal static List<OrderItem> OrderItem { get; } = new List<OrderItem> { };

    private static Random random = new Random(DateTime.Now.Millisecond);

    /// <summary>
    /// 
    /// </summary>
    static DataSource() 
    {
        s_Initialize();
    }
    /// <summary>
    /// 
    /// </summary>
    static private void addNewProduct()
    {
        List<List<string>> ProductsNames = new List<List<string>>
            {
                new List<string>{ "Chevre Cheese Mold", "Traditional Basket Cheese Mold","Hard Cheese Mold"},
                new List<string>{"Dairy thermometer" },
                new List<string>{"Measuring cups and spoons"},
                new List<string>{"Curd ladle","Curd knife" },
                new List<string>{"pH and acid testing equipment" }
            };

        for (int i = 0; i < ProductsNames.Count(); i++)
        {
            Enums.Category category = (Enums.Category)i;
            int pres = (int)(ProductsNames[i].Count() * 0.05 + 1);

            foreach (var name in ProductsNames[i])
            {
                Product _product = new Product
                {
                    ID = config.NextOrderNumberOrderItem,
                    Name = name,
                    Categories = category, 
                    Price = random.Next(100) + 0.90, 
                    InStock = pres-- > 0 ? 0 : random.Next(30, 100),
                };

                Products.Add(_product);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    static private void addOrderItem()
    {
        string[] Names = new string[] {"Norit","Ron","Yael","Lior","Shlomo","Tamar","harry","Jacob","Lili","Rivka","Tal","Aharon","David"};
        string[] Addresses = new string[] {"Heifa", "Ashkelon", "Ra'anana", "Eylat", "Jerusalem", "Tel Aviv", "Be'er Seva", "Bet Shemesh", "Ashdod","Dimona"};


        for (int i = 0; i < 10; i++)
        {
            int percent_shiped = (int)(orders.Count() * 0.8 + 1);
            int percent_deliverd = (int)(percent_shiped * 0.6 + 1);
            string newCustomerName = Names[i];

            Order newOrder = new Order
            {
                ID = config.NextOrderNumberOrderItem,
                CustomerName = newCustomerName,
                CustomerEmail = newCustomerName + "@gmail.com",
                CustomerAddress = Addresses[i],
                OrderDate = DateTime.Now - new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)),
                ShipDate = percent_shiped-- > 0 ? DateTime.MinValue + new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)) : DateTime.MinValue,
                DeliveryDate = percent_deliverd-- > 0 ? DateTime.MinValue + new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)) : DateTime.MinValue,
            };
            orders.Add(newOrder);

        }
    }
    /// <summary>
    /// 
    /// </summary>
    static private void addOrders()
    {
        for (int i = 0; i < 10; i++)
        {
            int orderId = random.Next(0, orders.Count);
            for (int j = 0; j <random.Next(1,10); j++)
            {
                Product? product = Products[random.Next(Products.Count)]; //choose random product to put into the orderitems list
                OrderItem _orderItem = new OrderItem
                {
                    ID = config.nextOrderNumber,
                    OrderID = orderId,
                    ProductID = product?.ID ?? 0,
                    Price = product?.Price ?? 0,
                    Amount = random.Next(1, 10),
                };
                OrderItem.Add(_orderItem);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    static private void s_Initialize()
    {
        addNewProduct();
        addOrderItem();
        addOrders();
    }
}
