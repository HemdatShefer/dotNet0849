using DO;

namespace Dal;

class Program
{
    private static DalProduct _Products = new DalProduct();
    private static DalOrderItem _items = new DalOrderItem();
    private static DalOrder _order = new DalOrder();

    private static void printAll<T>(IEnumerable<T> values)
    {
        Console.WriteLine(string.Join('\n', values));
    }

    public static char printMenu()
    {
        Console.WriteLine("a - add object");
        Console.WriteLine("b - print object");
        Console.WriteLine("c - print all object data");
        Console.WriteLine("d - update object");
        Console.WriteLine("e - Delete object");

        Console.WriteLine("Enter your choice: ");
        return char.Parse(Console.ReadLine());
    }
    public static void ProductMenu()
    {
        char choice = printMenu();
        Product p = new Product();
        int id;
        string name;
        Enums.Category category;
        int price;
        int inStock; 

        switch (choice)
        {
            case 'a':
                Console.WriteLine("Enter values to add object (id, name, cateegory, price, instock): ");

                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Wrong input enter again");
                }
                p.ID = id;
                p.Name = Console.ReadLine();
                p.Categories = (Enums.Category)(Convert.ToInt32(Console.ReadLine()));
                p.Price = double.Parse(Console.ReadLine());
                p.InStock = int.Parse(Console.ReadLine());
                try
                {
                    _Products.Add(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'b':
                Console.WriteLine("Enter the ID of the product: ");
                try
                {
                    int ID = int.Parse(Console.ReadLine());
                    p = _Products.get_product(ID);
                    Console.WriteLine(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'c':
                printAll(_Products.GetAll());
                break;

            case 'd':
                Console.WriteLine("enter values to update object:");
            
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Wrong number enter again");
                }
                p.ID = id;
                p.Name = Console.ReadLine();
                p.Categories = (Enums.Category)(Convert.ToInt32(Console.ReadLine()));
                p.Price = double.Parse(Console.ReadLine());
                p.InStock = int.Parse(Console.ReadLine());

                try
                {
                    _Products.Add(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'e':
                Console.WriteLine("Enter the ID of the product: ");
                id = int.Parse(Console.ReadLine());
                try
                {
                    _Products.Delete(id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            default:
                Console.WriteLine("input not courrect");

                break;
        }
    }

    public static void OrderItemMenu()//תת תפריט של פריט 
    {
        char choice = printMenu();
        OrderItem orderItem = new OrderItem();

        switch (choice)
        {
            case 'a':
                int id;

                Console.WriteLine("enter values to add object:");
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Wrong number enter again");
                }
                orderItem.ID = id;
                orderItem.ProductID = int.Parse(Console.ReadLine());
                orderItem.OrderID = int.Parse(Console.ReadLine());
                orderItem.Price = double.Parse(Console.ReadLine());
                orderItem.Amount = int.Parse(Console.ReadLine());

                try
                {
                    Program._items.Add(orderItem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                break;

            case 'b':
                Console.WriteLine("Enter the ID of the object: ");
                id = int.Parse(Console.ReadLine());
                try
                {
                    OrderItem pr = Program._items.get(id);
                    Console.WriteLine(pr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'c':
                printAll(Program._items.GetItemslist());
                break;

            case 'd':
                Console.WriteLine("enter values to update object:");
                orderItem.ID = int.Parse(Console.ReadLine());
                orderItem.ProductID = int.Parse(Console.ReadLine());
                orderItem.OrderID = int.Parse(Console.ReadLine());
                orderItem.Price = double.Parse(Console.ReadLine());
                orderItem.Amount = int.Parse(Console.ReadLine());
                try
                {
                    Program._items.update(orderItem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'e':
                Console.WriteLine("Enter the ID of the object: ");
                int ID = int.Parse(Console.ReadLine());
                try
                {
                    Program._items.delete(ID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            default:
                break;
        }

    }


    public static void OrderMenu()//תת תפריט של הזמנה
    {
        char choice = printMenu();
        Order order = new Order();

        switch (choice)
        {
            case 'a':
                Console.WriteLine("enter values to add object:");
                order.ID = int.Parse(Console.ReadLine());
                order.CustomerName = Console.ReadLine();
                order.CustomerEmail = Console.ReadLine();
                order.CustomerAddress = Console.ReadLine();
                order.OrderDate = DateTime.Parse(Console.ReadLine());
                order.ShipDate = DateTime.Parse(Console.ReadLine());
                order.DeliveryDate = DateTime.Parse(Console.ReadLine());

                try
                {
                    _order.Add(order);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'b':
                try
                {
                    int ID = int.Parse(Console.ReadLine());
                    order = _order.GetOrder(ID);
                    Console.WriteLine(order);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'c':
                printAll(Program._order.GetAll());
                break;
            case 'd':
                Console.WriteLine("enter values to update object:");
                order.ID = int.Parse(Console.ReadLine());
                order.CustomerName = Console.ReadLine();
                order.CustomerEmail = Console.ReadLine();
                order.CustomerAddress = Console.ReadLine();
                order.OrderDate = DateTime.Parse(Console.ReadLine());
                order.ShipDate = DateTime.Parse(Console.ReadLine());
                order.DeliveryDate = DateTime.Parse(Console.ReadLine());

                try
                {
                    Program._order.Update(order);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                break;

        }

    }
        static void Main(string[] args)
        {
        try
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Enter your choice from 0-3:");
                char ch = char.Parse(Console.ReadLine());

                if (ch == '0')
                {
                    return;
                }
                if (ch == '1')
                {
                    ProductMenu();
                }
                if (ch == '2')
                {
                    OrderItemMenu();
                }
                if (ch == '3')
                {
                    OrderMenu();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
        }
    }




