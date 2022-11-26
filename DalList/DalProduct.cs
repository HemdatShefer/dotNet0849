using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DO;


namespace Dal;

public class DalProduct
{
    public int Add(Product p)
    {
        DataSource.Products.Add(p);
        return p.ID;
    }
    
    public Product get_product(int ID)
    {
        foreach (Product p in DataSource.Products)
        {
            if(p.ID == ID)
            {
                return p;   
            }
        }
        throw new Exception("CANT FIND ITEM");
    }

    public void Delete(int id)
    {
        foreach (Product p in DataSource.Products)
        {
            if (p.ID == id)
            {
                DataSource.Products.Remove(p);
                break;
            }
        }
        throw new Exception("CANT FIND ITEM");
    }
    public bool Exist(int id)
    {
      
        return DataSource.Products.Exists(product => product.ID == id);
    }

    public IEnumerable<Product> GetAll()
    {
        return from Product item in DataSource.Products select item;
    }


    public void update(Product p)
    {
        Delete(p.ID);
        Add(p);
    }
}
