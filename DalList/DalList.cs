using DalApi;

namespace Dal
{
    /// <summary>
    /// A sealed class implementing IDal to ensure it is accessed via a singleton instance.
    /// This ensures only one instance of the DAL is created, providing a centralized point of data access.
    /// </summary>
    sealed internal class DalList : IDal
    {
        /// <summary>
        /// The singleton instance of the DAL.
        /// </summary>
        public static IDal Instance { get; } = new DalList();

        private DalList() { }    // Private constructor to prevent instantiation outside this class.
        public IOrder Order => new DalOrder();

        public IProduct Product => new DalProduct();

        public IOrderItem OrderItem => new DalOrderItem();
    }
}
