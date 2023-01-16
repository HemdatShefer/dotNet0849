using BO;
using Dal;
using DalApi;
using IOrder = BlApi.IOrder;

namespace BlImplementation
{

    internal class Order : IOrder
    {
        DalApi.IDal? _dal = DalApi.Factory.Get();
        /// <summary>
                                                        /// 
                                                        /// </summary>
                                                        /// <returns></returns>
        IEnumerable<OrderForList> IOrder.orderForLists()
        {
            IEnumerable<DO.Order> ordersList = new Dal.DalOrder().GetAll();
            List<BO.OrderForList> ordersForList = new List<BO.OrderForList>();
            
            foreach (DO.Order order in ordersList)
            {
                IEnumerable<DO.OrderItem> ordersItems = _dal!.OrderItem.GetAll().Where(orderItem => orderItem.OrderID == order.ID);
                ordersForList.Add(new BO.OrderForList
                {
                    ID = order.ID,
                    Name = order.CustomerName,
                    Status = OrderStatus(order),
                    Amount = ordersItems.Count(),
                    TotalPrice = GetTotalPrice(ordersItems)
                });
            }
            return ordersForList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        /// <exception cref="unValidException"></exception>
        BO.Order IOrder.GetOrder(int orderID)
        {
            if (validID(orderID))
            {
                DO.Order order = _dal!.Order.GetById(orderID);
                IEnumerable<DO.OrderItem> orderItems = _dal.OrderItem.GetAll().Where(orderItem => orderItem.OrderID == orderID);

                return new BO.Order
                {
                    ID = order.ID,
                    CustomerName = order.CustomerName,
                    CustomerAddress = order.CustomerAddress,
                    OrderDate = order.OrderDate,
                    CustomerEmail = order.CustomerEmail,
                    ShipDate = order.ShipDate,
                    DeliveryDate = order.DeliveryDate,
                    Status = OrderStatus(order),
                    TotalPrice = orderItems.Sum(orderItem => orderItem.Price * orderItem.Amount),
                    Items = orderItems.Select(orderItem => new BO.OrderItem
                    {
                        ID = orderItem.ID,
                        ProductID = orderItem.ProductID,
                        OrderID = orderItem.OrderID,
                        Price = orderItem.Price,
                        Amount = orderItem.Amount,
                        TotalPrice = orderItem.Price * orderItem.Amount,
                    }).ToList()
                };
            }
            else
            {
                throw new unValidException("id not valid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        /// <exception cref="unValidException"></exception>
        BO.Order IOrder.UpdateshippedDate(int orderID)
        {
            try
            {
                DO.Order order = _dal!.Order.GetById(orderID);
                IEnumerable<DO.OrderItem> orderItems = _dal.OrderItem.GetAll();
                IEnumerable<DO.OrderItem> orderItemsFilter = _dal.OrderItem.GetAll();
                return new BO.Order
                {
                    ID = order.ID,
                    CustomerName = order.CustomerName,
                    CustomerAddress = order.CustomerAddress,
                    OrderDate = order.OrderDate,
                    CustomerEmail = order.CustomerEmail,
                    ShipDate = order.ShipDate,
                    DeliveryDate = order.DeliveryDate,
                    Status = BO.Enums.Status.shipped,
                    TotalPrice = orderItems.Sum(orderItem => orderItem.Price * orderItem.Amount),
                    Items = orderItemsFilter.Select(orderItem => new BO.OrderItem
                    {
                        ID = orderItem.ID,
                        ProductID = orderItem.ProductID,
                        OrderID = orderItem.OrderID,
                        Price = orderItem.Price,
                        Amount = orderItem.Amount
                    }).ToList()
                };
            }
            catch
            {
                throw new unValidException("id not valid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        /// <exception cref="unValidException"></exception>
        BO.Order IOrder.UpdateDeliverdDate(int orderID)
        {
            try
            {
                DO.Order order = _dal!.Order.GetById(orderID);
                IEnumerable<DO.OrderItem> orderItems = _dal!.OrderItem.GetAll();
                IEnumerable<DO.OrderItem> orderItemsFilter = _dal!.OrderItem.GetAll();
                return new BO.Order
                {
                    ID = order.ID,
                    CustomerName = order.CustomerName,
                    CustomerAddress = order.CustomerAddress,
                    OrderDate = order.OrderDate,
                    CustomerEmail = order.CustomerEmail,
                    ShipDate = order.ShipDate,
                    DeliveryDate = order.DeliveryDate,
                    Status = BO.Enums.Status.deliverd,
                    TotalPrice = orderItems.Sum(orderItem => orderItem.Price * orderItem.Amount),
                    Items = orderItemsFilter.Select(orderItem => new BO.OrderItem
                    {
                        ID = orderItem.ID,
                        ProductID = orderItem.ProductID,
                        OrderID = orderItem.OrderID,
                        Price = orderItem.Price,
                        Amount = orderItem.Amount
                    }).ToList()
                };
            }
            catch
            {
                throw new unValidException("id not valid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        /// <exception cref="unValidException"></exception>
        BO.OrderTracking IOrder.GetOrderTracking(int orderID)
        {
            try
            {
                DO.Order order = _dal!.Order.GetById(orderID);
                return new OrderTracking()
                {
                    ID = orderID,
                    Status = OrderStatus(order),
                    OrderTrackingStatus = GetStringStatus(order),
                };
            }
            catch
            {
                throw new unValidException("id not valid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        string GetStringStatus(DO.Order order)
        {
            if (order.OrderDate != DateTime.MinValue)
            {
                string? date_str = order.OrderDate.ToString("dd/MM/yyyy HH:mm:ss");
                return date_str + " order created";
            }
            if (order.ShipDate != DateTime.MinValue)
            {
                string? date_str = order.ShipDate.ToString("dd/MM/yyyy HH:mm:ss");
                return date_str + " order sent";
            }
            if (order.DeliveryDate != DateTime.MinValue)
            {
                string date_str = order.DeliveryDate.ToString("dd/MM/yyyy HH:mm:ss");
                return date_str + " order deliverd";
            }
            return " order created";


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        private static bool validID(int orderID)
        {
            return orderID > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisOrder"></param>
        /// <returns></returns>
        private static BO.Enums.Status OrderStatus(DO.Order thisOrder)
        {
            BO.Enums.Status status = BO.Enums.Status.confirmed;
            if (thisOrder.OrderDate != DateTime.MinValue)
            {
                status = BO.Enums.Status.confirmed;
            }
            if (thisOrder.ShipDate != DateTime.MinValue)
            {
                status = BO.Enums.Status.shipped;
            }
            if (thisOrder.DeliveryDate != DateTime.MinValue)
            {
                status = BO.Enums.Status.deliverd;
            }

            return status;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrdersItems"></param>
        /// <returns></returns>
        private static double GetTotalPrice(IEnumerable<DO.OrderItem> OrdersItems)
        {
            double price = 0;
            foreach (DO.OrderItem thisOrderItem in OrdersItems)
            {
                price += thisOrderItem.Price * thisOrderItem.Amount;
            }
            return price;
        }

    }
}