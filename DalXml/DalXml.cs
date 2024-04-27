using DalApi;

namespace Dal
{
    sealed internal class DalXml :IDal
    {
        public static IDal Instance { get; } = new DalXml();

        private DalXml() { }

        public IProduct Product { get; } = new DalProduct();

        public IOrder Order { get; } = new DalOrder();

        public IOrderItem OrderItem { get; } = new DalOrderItem();

    }
}