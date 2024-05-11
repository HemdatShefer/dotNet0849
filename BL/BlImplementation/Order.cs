using BO;
using Dal;
using DalApi;
using static BO.Enums;
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
        /// 
        public IEnumerable<OrderForList?> orderForLists()
        {
            IEnumerable<DO.Order> ordersList = new Dal.DalOrder().GetAll();
            return ordersList.Select(order =>
            {
                // Retrieve all order items for the current order
                IEnumerable<DO.OrderItem> ordersItems = _dal!.OrderItem.GetAll()
                    .Where(orderItem => orderItem.OrderID == order.ID);

                return new BO.OrderForList
                {
                    ID = order.ID,
                    Name = order.CustomerName,
                    Status = OrderStatus(order),
                    Amount = ordersItems.Count(),
                    TotalPrice = GetTotalPrice(ordersItems)
                };
            }).ToList(); // Convert the result to List to match the return type IEnumerable
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
                        Total = orderItem.Price * orderItem.Amount,
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
                BO.Order order1 = new BO.Order
                {
                    ID = order.ID,
                    CustomerName = order.CustomerName,
                    CustomerAddress = order.CustomerAddress,
                    OrderDate = order.OrderDate,
                    CustomerEmail = order.CustomerEmail,
                    ShipDate = DateTime.Now,
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
                _dal.Order.Delete(orderID);
                _dal.Order.Add(new DO.Order
                {
                    ID = orderID,
                    CustomerName = order.CustomerName,
                    CustomerAddress = order.CustomerAddress,
                    OrderDate = order.OrderDate,
                    CustomerEmail = order.CustomerEmail,
                    ShipDate = DateTime.Now,
                    DeliveryDate = order.DeliveryDate,
                });
                return order1;
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
                BO.Order order1 = new BO.Order
                {
                    ID = order.ID,
                    CustomerName = order.CustomerName,
                    CustomerAddress = order.CustomerAddress,
                    OrderDate = order.OrderDate,
                    CustomerEmail = order.CustomerEmail,
                    ShipDate = order.ShipDate,
                    DeliveryDate = DateTime.Now,
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
                _dal.Order.Delete(orderID);
                _dal.Order.Add(new DO.Order
                {
                    ID = order.ID,
                    CustomerName = order.CustomerName,
                    CustomerAddress = order.CustomerAddress,
                    OrderDate = order.OrderDate,
                    CustomerEmail = order.CustomerEmail,
                    ShipDate = order.ShipDate,
                    DeliveryDate = DateTime.Now,

                });
                return order1;
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
                List<Tuple<Enums.Status, DateTime>>? orderTrackingStatus = new List<Tuple<Enums.Status, DateTime>>();

                if (order.OrderDate != DateTime.MinValue)
                    orderTrackingStatus.Add(Tuple.Create(Enums.Status.confirmed, order.OrderDate));

                if (order.ShipDate != DateTime.MinValue)
                    orderTrackingStatus.Add(Tuple.Create(Enums.Status.shipped, order.ShipDate));

                if (order.DeliveryDate != DateTime.MinValue)
                    orderTrackingStatus.Add(Tuple.Create(Enums.Status.deliverd, order.DeliveryDate));

                return new OrderTracking()
                {
                    ID = orderID,
                    Status = OrderStatus(order),
                    OrderTrackingStatus = orderTrackingStatus,
                }; ;
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
            return OrdersItems.Sum(item => item.Price * item.Amount);

        }

        public BO.Order OldOrder()
        {
            IEnumerable<IGrouping<int, DO.OrderItem>> orderItems = from item in _dal.OrderItem.GetAll()
                                                                   group item by item.OrderID into g
                                                                   select g;

            IEnumerable<BO.Order> orders = from order in _dal.Order.GetAll()
                                           select new BO.Order
                                           {
                                               ID = order.ID,
                                               CustomerName = order.CustomerName,
                                               CustomerEmail = order.CustomerEmail,
                                               CustomerAddress = order.CustomerAddress,
                                               OrderDate = order.OrderDate,
                                               ShipDate = order.ShipDate,
                                               DeliveryDate = order.DeliveryDate,
                                               Status = GetStatus(order),
                                               Items = GetOrderItemList(order.ID).ToList()!,
                                               TotalPrice = (double)(from item in orderItems
                                                                     where item.Key == order.ID
                                                                     select item).Sum(x => x.Sum(x => x.Price))!
                                           }; ;
            return orders.OrderBy(x => x.Status).ThenBy(x => x.OrderDate).First();
        }

        private Status GetStatus(DO.Order? dOrder)
        {
            return dOrder?.DeliveryDate != DateTime.MinValue ? Status.deliverd :
                dOrder?.ShipDate != DateTime.MinValue ? Status.shipped : Status.confirmed;
        }

        /// <summary>
        /// return dOrder item list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private IEnumerable<BO.OrderItem> GetOrderItemList(int? id)
        {
            try
            {
                return from DO.OrderItem item in _dal.OrderItem.GetAll().Where(x => x.OrderID == id)
                       select new BO.OrderItem
                       {
                           OrderID = item.ID,
                           ProductID = item.ProductID,
                           Amount = item.Amount,
                           Price = item.Price / item.Amount,
                           Total = item.Price
                       };
            }
            catch (Exception ex)
            {
                throw new ObjectNotFoundException("the list is empty\n");
            }
        }
    }



}