using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal;
public class DalOrder 
{
    public int Add(Order item)
    {
        DataSource.orders.Add(item);
        return item.ID;
    }
 
    public void Delete(int id)
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


    public IEnumerable<Order> GetAll()
    {
        return (from Order item in DataSource.orders select item).ToList();
    }

    public Order GetOrder(int id)
    {
        foreach(Order order in DataSource.orders)
        {
            if(order.ID == id)
            {
                return order;   
            }
        }
        throw new Exception("CANT FIND order");
    }
    public void Update(Order item)
    {
        Delete(item.ID);
        Add(item);
    }
}
