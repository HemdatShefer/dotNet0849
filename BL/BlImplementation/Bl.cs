using BlApi;

namespace BlImplementation
{
    public class Bl : IBl
    {
        public ICart Cart { get; } = new Cart();
        public IProduct Product { get; } = new Product();
        public IOrder Order { get; } = new Order();

    }
}