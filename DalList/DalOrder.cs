using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace Dal;

public class DalOrder : IOrder
{

    public static int Add(Order item)
    {
        DataSource.orders.Add(item);
        return item.ID;
    }

    public static void Delete(int id)
    {
        foreach (Order order in DataSource.orders)
        {
            if (order.ID == id)
            {
                DataSource.orders.Remove(order);
            }
        }
        throw new Exception("CANT FIND order");
    }


    public static IEnumerable<Order?>  GetAll()
    {
        return (IEnumerable<Order?>)(from Order item in DataSource.orders select item).ToList();
    }

    public static Order GetByIdOrder(int id)
    {
        foreach (var order in DataSource.orders.Where(order => order.ID == id)){ return order;}
        throw new Exception("CANT FIND order");
    }

    public static void Update(Order item)
    {
        Delete(item.ID);
        Add(item);
    }

    Order ICrud<Order>.GetById(int id)
    {
        foreach (var order in DataSource.orders.Where(order => order.ID == id)) { return order; }
        throw new NotImplementedException("CANT FIND order");
    }

    IEnumerable<Order> ICrud<Order>.GetAll()
    {
        return (from Order item in DataSource.orders select item).ToList();
    }

    public void AddOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public void DeleteOrder(int IDorder)
    {
        throw new NotImplementedException();
    }

    public void UpdateOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Order GetProduct(int IDorder)
    {
        throw new NotImplementedException();
    }

    public int Add(Order item)
    {
        throw new NotImplementedException();
    }

    public void Update(Order item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}
