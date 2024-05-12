using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation
{

    internal class Cart : ICart
    {
        DalApi.IDal? _dal = DalApi.Factory.Get();

        /// <summary>
        /// /
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <exception cref="NotInStockException"></exception>
        /// <exception cref="ObjectNotFoundException"></exception>
        public BO.Cart AddOrderItem(BO.Cart cart, int productId)
        {
            try
            {
                DO.Product dOproduct = _dal!.Product.GetById(productId);

                if (dOproduct.InStock <= 0)
                    throw new NotInStockException("stock is empty");

                OrderItem orderItem = getOrderItem(cart, productId);

                if (orderItem is null)
                    cart.Items!.Add(new BO.OrderItem { Price = dOproduct.Price, Amount = 1, ProductID = dOproduct.ID, ID = productId, Total = dOproduct.Price });

                else
                {
                    orderItem.Amount++;
                    orderItem.Total = orderItem.Price* orderItem.Amount;
                }

                cart.TotalPrice += dOproduct.Price;
            }

            catch (DO.ObjectNotFoundException ex)
            {
                throw new ObjectNotFoundException("", ex);
            }
            return cart;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        private OrderItem getOrderItem(BO.Cart cart, int productId)
        {
            if (cart.Items == null)
            {
                return null;
            }
            foreach (OrderItem orderItem in cart.Items)
            {
                if (orderItem.ProductID == productId)
                {
                    return orderItem;
                }
            }
            return null!;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// <exception cref="unValidException"></exception>
        public BO.Cart UpdateOrderItem(BO.Cart cart, int productId, int amount)
        {
            try
            {
                OrderItem orderItem = getOrderItem(cart, productId);

                if (orderItem is not null)
                {
                    DO.Product DOproduct = _dal!.Product.GetById(productId);

                    if (DOproduct.InStock - amount <= 0)
                    {
                        throw new NotInStockException("Not enough products in stock");
                    }

                    if (amount == 0)
                    {
                        cart.Items!.Remove(orderItem);
                    }
                    else
                    {
                        cart.TotalPrice += -(orderItem.Price * orderItem.Amount) + orderItem.Price * amount;

                        orderItem.Amount = amount;
                    }
                }
            }
            catch(NotInStockException )
            {
                throw new NotInStockException("Not enough products in stock");
            }
            catch(ObjectNotFoundException)
            { 
            }
            return cart;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <exception cref="EmptyCartException"></exception>
        public void CommitCart(BO.Cart cart)
        {
            if (cart.Items!.Count() <= 0)
            {
                throw new EmptyCartException("cart are empty");
            }
            try
            {
                CheckClient(cart);
                List<DO.Product> products = cart.Items!.Select(item =>
                {
                    CheckProduct(item); // Performs product checks
                    return _dal!.Product.GetById(item.ProductID); // Retrieves the product
                }).ToList(); // Converts the IEnumerable returned by Select to List

                int OrderId = _dal!.Order.Add(new DO.Order
                {
                    ID = 0,
                    CustomerName = cart.CustomerName!,
                    CustomerAddress = cart.CustomerAdress!,
                    CustomerEmail = cart.CustomerEmail!,
                    OrderDate = DateTime.Now,
                    DeliveryDate = DateTime.MinValue,
                    ShipDate = DateTime.MinValue
                });
                cart.Items.ForEach(orderItem =>
                {
                    _dal.OrderItem.Add(new DO.OrderItem
                    {
                        OrderID = OrderId,
                        ProductID = orderItem.ProductID,
                        Price = orderItem.Price,
                        Amount = orderItem.Amount,
                        ID= orderItem.ProductID,
                    });

                    DO.Product product = products.Find(product => product.ID == orderItem.ProductID);
                    product.InStock -= orderItem.Amount;

                    _dal.Product.Update(product);
                });
                cart.Items.Clear();
                cart.TotalPrice = 0;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <exception cref="EmailUnvalidException"></exception>
        /// <exception cref="NameUnvalidException"></exception>
        /// <exception cref="AdressUnvalidException"></exception>
        /// <exception cref="TotalUnvalidException"></exception>
        public static void CheckClient(BO.Cart cart)
        {

            if (cart.CustomerEmail == "")
            {
                throw new EmailUnvalidException("invalid Customer Email");
            }
            if (cart.CustomerName == "")
            {
                throw new NameUnvalidException("invalid Name");

            }
            if (cart.CustomerAdress == "")
            {
                throw new AdressUnvalidException("invalid Customer Adress");

            }
            if (cart.TotalPrice < 0)
            {
                throw new TotalUnvalidException("invalid total price");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="IDNotAdmissibleException"></exception>
        /// <exception cref="NameNotAdmissibleException"></exception>
        /// <exception cref="PriceNotAdmissibleException"></exception>
        private static void CheckProduct(BO.OrderItem product)
        {
            if (product.ProductID <= 0)
            {
                throw new IDNotAdmissibleException("invalid ID input");
            }
            if (product.Amount <= 0)
            {
                throw new NameNotAdmissibleException("invalid name input");
            }
            if (product.Price < 0)
            {
                throw new PriceNotAdmissibleException("invalid price input");
            }

        }
    }

}

