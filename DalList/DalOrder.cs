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

    public Order GetByIdOrder(int id)
    {
        foreach (var order in DataSource.orders.Where(order => order.ID == id)){ return order;}
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
}
