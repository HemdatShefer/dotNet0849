using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal;

/// <summary>
/// Provides CRUD operations for Order entities by implementing the IOrder interface.
/// </summary>
public class DalOrder : IOrder
{
    // Implementation details for Add, Delete, GetAll, GetById, and Update methods
    // which interact with an in-memory DataSource. Demonstrates handling of ID generation and entity retrieval.
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order order)
    {
        if(order.ID == 0)
        {
            order.ID = DataSource.config.NextOrderNumberOrderItem;
        }
        DataSource.orders.Add(order);
        return order.ID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        foreach (Order order in DataSource.orders)
        {
            if (order.ID == id)
            {
                DataSource.orders.Remove(order);
                return;
            }
        }
        throw new Exception("CANT FIND order");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> GetAll()
    {
        return (from Order item in DataSource.orders select item).ToList();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order GetByIdOrder(int id)
    {
        foreach (var order in DataSource.orders.Where(order => order.ID == id)) { return order; }
        throw new Exception("CANT FIND order");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order item)
    {
        Delete(item.ID);
        Add(item);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order GetById(int id)
    {
        foreach (var order in DataSource.orders.Where(order => order.ID == id)) { return order; }
        throw new ObjectNotFoundException("CANT FIND order");
    }

}
