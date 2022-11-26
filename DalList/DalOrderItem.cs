using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalOrderItem//נתון פריט בהזמנה
{
    public int Add(OrderItem item)
    {
        DataSource.OrderItem.Add(item);
        return item.OrderID;
    }

    public OrderItem get(int id)
    {
        foreach (OrderItem item in DataSource.OrderItem)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        throw new Exception("CANT FIND ITEM");
    }

    public List<OrderItem> GetItemslist()
    {
        return (from OrderItem item in DataSource.OrderItem select item).ToList();
    }
    public void delete(int id)
    {
        foreach (OrderItem o in DataSource.OrderItem)
        {
            if (o.ID == id)
            {
                DataSource.OrderItem.Remove(o);
                return;
            }
        }
        throw new Exception("CANT FIND ITEM");
    }
    public bool Exist(int id)
    {
        foreach (OrderItem item in DataSource.OrderItem)
        {
            if (item.ID == id)
            {
                return true;
            }
        }
        return false;
    }
    public void update(OrderItem item)//מתודת עדכון
    {
        delete(item.ID);
        Add(item);
    }

}