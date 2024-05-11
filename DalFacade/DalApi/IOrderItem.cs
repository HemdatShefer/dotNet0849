using DO;

namespace DalApi
{
    /// <summary>
    /// Represents the CRUD operations for OrderItem entities.
    /// Extends ICrud for specific data handling related to order items.
    /// </summary>
    public interface IOrderItem : ICrud<OrderItem>
    {
    }
}
