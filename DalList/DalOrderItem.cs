using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal;
/// <summary>
/// Implements IOrderItem for CRUD operations specific to OrderItem entities.
/// </summary>
public class DalOrderItem : IOrderItem
{
    // Methods implement the CRUD operations handling in-memory data access through DataSource.
    [MethodImpl(MethodImplOptions.Synchronized)]
    int ICrud<OrderItem>.Add(OrderItem item)
    {
        DataSource.OrderItem.Add(item);
        return item.OrderID;
    }
    OrderItem ICrud<OrderItem>.GetById(int id)
    {
        foreach (var item in DataSource.OrderItem.Where(item => item.ID == id))
        {
            return item;
        }
        throw new Exception("CANT FIND ITEM");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    IEnumerable<OrderItem> ICrud<OrderItem>.GetAll()
    {
        return (from OrderItem item in DataSource.OrderItem select item).ToList();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem?> GetAll()
    {
        return (from OrderItem? item in DataSource.OrderItem select item).ToList();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    void ICrud<OrderItem>.Delete(int id)
    {
        foreach (var order in DataSource.OrderItem.Where(o => o.ID == id))
        {
            DataSource.OrderItem.Remove(order);
            return;
        }

        throw new Exception("CANT FIND ITEM");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    private bool existItem(int id)
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    void ICrud<OrderItem>.Update(OrderItem item)
    {
        foreach (var order in DataSource.OrderItem.Where(o => o.ID == item.ID))
        {
            DataSource.OrderItem.Remove(order);
            return;
        };
        DataSource.OrderItem.Add(item);
    }


}