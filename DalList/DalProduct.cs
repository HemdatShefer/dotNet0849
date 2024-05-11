using DalApi;
using DO;


namespace Dal;

/// <summary>
/// Implements IProduct for managing Product entities' CRUD operations.
/// This includes custom validation like checking the existence of an item before adding to prevent duplicates.
/// </summary>
public class DalProduct : IProduct
{
    // Implementation details for Add, Delete, GetById, GetAll, and Update.
    // Includes existence checks and handling of unique ID generation and data consistency.

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
        Product product = DataSource.Products.SingleOrDefault(p => p.ID == ID);
        if (Equals(product, default(Product)))  // Adjust for value types
        {
            throw new ObjectNotFoundException("Cannot find item with ID: " + ID);
        }
        return product;

    }

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <exception cref="ObjectNotFoundException">Thrown when no product with the specified ID can be found to delete.</exception>
    public void Delete(int id)
    {
        var product = DataSource.Products.FirstOrDefault(p => p.ID == id);
        if (Equals(product, default(Product)))  // Adjust for value types
        {
            throw new ObjectNotFoundException("Cannot find item to delete with ID: " + id);
        }
        DataSource.Products.Remove(product);
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

