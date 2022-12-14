using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;


namespace Dal;

/// <summary>
/// 
/// </summary>
public class DalProduct : IProduct
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public int Add(Product p)
    {
        DataSource.Products.Add(existItem(p.ID) ? throw new AlreadyExistException("This item is already exist") : p);
        return p.ID;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetById(int ID)
    {
        foreach (var p in DataSource.Products.Where(p => p.ID == ID))
        {
            return p;
        }

        throw new ObjectNotFoundException("CANT FIND ITEM");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        foreach (var p in DataSource.Products.Where(p => p.ID == id))
        {
            DataSource.Products.Remove(p);
            break;
        }

        throw new ObjectNotFoundException("CANT FIND ITEM");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool existItem(int id)
    {
        return DataSource.Products.Exists(product => product.ID == id);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product> GetAll()
    {
        return from Product item in DataSource.Products select item;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    public void Update(Product p)
    {
        Delete(p.ID);
        Add(p);
    }
}

