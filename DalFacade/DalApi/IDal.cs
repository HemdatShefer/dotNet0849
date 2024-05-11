namespace DalApi
{
    /// <summary>
    /// Defines a contract for data access layer implementations.
    /// This interface acts as a central access point for various entities like orders, products, and order items.
    /// </summary>
    public interface IDal
    {
        IOrder Order { get; }
        IProduct Product { get; }
        IOrderItem OrderItem { get; }
    }
}
