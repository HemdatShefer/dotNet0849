using DalApi;
using DO;

namespace Dal;

public class DalOrder : IOrder
{
    public int Add(Order order)
    {
            //order.ID = DataSource.config.NextOrderNumberOrderItem;
        DataSource.orders.Add(order);
        return order.ID;
    }

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


    public IEnumerable<Order> GetAll()
    {
        return (from Order item in DataSource.orders select item).ToList();
    }

    public Order GetByIdOrder(int id)
    {
        foreach (var order in DataSource.orders.Where(order => order.ID == id)) { return order; }
        throw new Exception("CANT FIND order");
    }

    public void Update(Order item)
    {
        Delete(item.ID);
        Add(item);
    }

    public Order GetById(int id)
    {
        foreach (var order in DataSource.orders.Where(order => order.ID == id)) { return order; }
        throw new ObjectNotFoundException("CANT FIND order");
    }

}
