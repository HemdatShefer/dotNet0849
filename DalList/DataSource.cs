using DO;

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
    static private void initProduct()
    {
        List<List<string>> ProductsNames = new List<List<string>>
            {
                new List<string>{ "pearl golden necklace", "amber necklace","haze necklace", "14k pearl necklace", "14k gold rope necklace"},
                new List<string>{"bubble errings", "purple haze errings", "mini moon errings","roxen errings" ,"breeze errings"},
                new List<string>{"14k gold buble ring", "pearl ring", "braid ring", "dimond ring", "wedding rings silver"},
                new List<string>{"bridal bracelet","lia bracelet", "pearl bracelet", "mermaid bracelet"},
                new List<string>{ "jacob bracelet", "men chain", "signature ring" }
            };
        string basePath = AppDomain.CurrentDomain.BaseDirectory; // Get the base directory of the application
        string imagesPath = Path.Combine(basePath, "photos");

        for (int i = 0; i < ProductsNames.Count(); i++)
        {
            Enums.Category category = (Enums.Category)i;
            int pres = (int)(ProductsNames[i].Count() * 0.05 + 1); // Calculate the count of products not in stock

            foreach (var name in ProductsNames[i])
            {
                string imageName = name.Replace(" ", "_") + ".png"; // Replace spaces with underscores for file names
                string fullPath = Path.Combine(imagesPath, imageName); // Combine the images path with the image file name

                Product _product = new Product
                {
                    ID = config.NextOrderNumberOrderItem,
                    Name = name,
                    Categories = category,
                    Price = Math.Round(random.NextDouble() * (500 - 100) + 100, 2), // Random price between 100 to 500, rounded to 2 decimal places
                    InStock = pres-- > 0 ? 0 : random.Next(30, 100), // Set the stock count to 0 for 5% of products
                    Path = fullPath
                }; 

                Products.Add(_product);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    static private void initOrders()
    {
        string[] Names = new string[] { "Norit", "Ron", "Yael", "Lior", "Shlomo", "Tamar", "harry", "Jacob", "Lili", "Rivka", "Tal", "Aharon", "David" };
        string[] Addresses = new string[] { "Heifa", "Ashkelon", "Ra'anana", "Eylat", "Jerusalem", "Tel Aviv", "Be'er Seva", "Bet Shemesh", "Ashdod", "Dimona" };



        for (int i = 0; i < 20; i++)
        {
            int customerIndex = random.Next(Names.Length);
            string newCustomerName = Names[customerIndex];
            string address = Addresses[customerIndex % Addresses.Length]; // מודולו למספר הכתובות כדי למנוע IndexOutOfRangeException

            DateTime orderDate = DateTime.Now.AddDays(-random.Next(1, 30)); // תאריך הזמנה עד 30 ימים לפני היום
            DateTime shipDate = orderDate.AddDays(random.Next(1, 5)); // תאריך משלוח עד 5 ימים לאחר ההזמנה
            DateTime deliveryDate = shipDate.AddDays(random.Next(1, 10)); // תאריך מסירה עד 10 ימים לאחר המשלוח

            Order newOrder = new Order
            {
                ID = config.nextOrderNumber,
                CustomerName = newCustomerName,
                CustomerEmail = $"{newCustomerName}@gmail.com",
                CustomerAddress = address,
                OrderDate = orderDate,
                ShipDate = (i < 16) ? shipDate : DateTime.MinValue, // 80% יש להם תאריך משלוח
                DeliveryDate = (i < 12) ? deliveryDate : DateTime.MinValue, // 60% מהם שנשלחו יש להם תאריך מסירה
            };
            orders.Add(newOrder);
        }

    }
    /// <summary>
    /// 
    /// </summary>
    static private void initOrderItems()
    {
        for (int i = 0; i < 20; i++)
        {
            int orderId = orders[i].ID;
            int number = random.Next(1, 10);
            for (int j = 0; j < number; j++)
            {
                Product? product = Products[random.Next(Products.Count)];
                OrderItem _orderItem = new OrderItem
                {
                    ID = config.NextOrderNumberOrderItem,
                    OrderID = orderId,
                    ProductID = product?.ID ?? 0,
                    Price = product?.Price ?? 0,
                    Amount = random.Next(1, 20),
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
        initProduct();
        initOrders();
        initOrderItems();
        //XmlTools.SaveListToXMLSerializer<Order>(orders, @"orders.xml");
        //XmlTools.SaveListToXMLSerializer<OrderItem>(OrderItem, @"ordersItems.xml");
        //XmlTools.SaveListToXMLSerializer<Product>(Products, @"products.xml");

    }
}
