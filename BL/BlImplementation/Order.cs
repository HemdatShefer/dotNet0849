using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using DO;
using IOrder = BlApi.IOrder;

namespace BlImplementation
{

    public class Order : IOrder
    {
        IEnumerable<OrderForList> IOrder.orderForLists()
        {
            IEnumerable<DO.Order?> OrdersList = Dal.DalOrder.GetAll();
            List<BO.OrderForList> ordersForList = new List<BO.OrderForList>();

            BO.OrderForList CurrentOrder;
            foreach (DO.Order thisOrder in OrdersList)
            {
                IEnumerable<DO.OrderItem?> OrdersItems = Dal.DalOrderItem.GetAll();
                BO.Enums.Status status = BO.Enums.Status.confirmed;
                ordersForList.Add(new BO.OrderForList
                {
                    ID = thisOrder.ID,
                    Name = thisOrder.CustomerName,
                    Status = OrderStatus(thisOrder),
                    ToalPrice = GetTotalPrice(OrdersItems)
                });
            }
            return ordersForList;
        }
        BO.Order IOrder.GetOrderManger(int orderID)
        {
            if (validID(orderID))
            {
                DO.Order order = Dal.DalOrder.GetByIdOrder(orderID);
                IEnumerable<DO.OrderItem?> orderItems = Dal.DalOrderItem.GetAll();
                IEnumerable<DO.OrderItem?> orderItemsFilter = Dal.DalOrderItem.GetAll();
                foreach (var _ in orderItems.Where(item => item.Value.ID == orderID).Select(item => new { item })) { }

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
                    totalPrice = orderItems.Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount),
                    Items = orderItemsFilter.Select(orderItem => new BO.OrderItem
                    {
                        ID = orderItem.Value.ID,
                        ProductID = orderItem.Value.ProductID,
                        OrderID = orderItem.Value.OrderID,
                        Price = orderItem.Value.Price,
                        Amount = orderItem.Value.Amount
                    }).ToList()
                };
            }
            else
            {
                throw new unValidException("id not valid");
            }
        }
        BO.Order IOrder.GetOrderClient(int orderID)
        {
            if (validID(orderID))
            {
                DO.Order order = Dal.DalOrder.GetByIdOrder(orderID);
                IEnumerable<DO.OrderItem?> orderItems = Dal.DalOrderItem.GetAll();
                IEnumerable<DO.OrderItem?> orderItemsFilter = Dal.DalOrderItem.GetAll();
                foreach (var _ in orderItems.Where(item => item.Value.ID == orderID).Select(item => new { item })) { }

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
                    totalPrice = orderItems.Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount),
                    Items = orderItemsFilter.Select(orderItem => new BO.OrderItem
                    {
                        ID = orderItem.Value.ID,
                        ProductID = orderItem.Value.ProductID,
                        OrderID = orderItem.Value.OrderID,
                        Price = orderItem.Value.Price,
                        Amount = orderItem.Value.Amount
                    }).ToList()
                };
            }
            else
            {
                throw new unValidException("id not valid");
            }
        }
        BO.Order IOrder.UpdateDelivery(int orderID)
        {
            try
            {
                DO.Order order = Dal.DalOrder.GetByIdOrder(orderID);
                IEnumerable<DO.OrderItem?> orderItems = Dal.DalOrderItem.GetAll();
                IEnumerable<DO.OrderItem?> orderItemsFilter = Dal.DalOrderItem.GetAll();
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
                    totalPrice = orderItems.Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount),
                    Items = orderItemsFilter.Select(orderItem => new BO.OrderItem
                    {
                        ID = orderItem.Value.ID,
                        ProductID = orderItem.Value.ProductID,
                        OrderID = orderItem.Value.OrderID,
                        Price = orderItem.Value.Price,
                        Amount = orderItem.Value.Amount
                    }).ToList()
                };
            }
            catch
            {
                throw new unValidException("id not valid");
            }
        }
        BO.Order IOrder.UpdatSupplay(int orderID)
        {
            try
            {
                DO.Order order = Dal.DalOrder.GetByIdOrder(orderID);
                IEnumerable<DO.OrderItem?> orderItems = Dal.DalOrderItem.GetAll();
                IEnumerable<DO.OrderItem?> orderItemsFilter = Dal.DalOrderItem.GetAll();
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
                    totalPrice = orderItems.Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount),
                    Items = orderItemsFilter.Select(orderItem => new BO.OrderItem
                    {
                        ID = orderItem.Value.ID,
                        ProductID = orderItem.Value.ProductID,
                        OrderID = orderItem.Value.OrderID,
                        Price = orderItem.Value.Price,
                        Amount = orderItem.Value.Amount
                    }).ToList()
                };
            }
            catch
            {
                throw new unValidException("id not valid");
            }
        }
        BO.OrderTracking IOrder.GetOrderTracking(int orderID)
        {
            try
            {
                DO.Order order = Dal.DalOrder.GetByIdOrder(orderID);
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
        IEnumerable<BO.OrderForList?> GetProductsForList(Func <OrderForList?, bool>filter)
        {
            List<BO.OrderForList> ProductForList = new List<BO.OrderForList>();
            IEnumerable<DO.Order?> productsList = Dal.DalOrder.GetAll();
            return (IEnumerable<BO.OrderForList?>)(from ord in ProductForList where filter(ord) select ord).ToList();
        }



        string GetStringStatus(DO.Order order)
        {
            if (order.OrderDate != DateTime.MinValue )
            {
                string? date_str = order.OrderDate.ToString("dd/MM/yyyy HH:mm:ss");
                return date_str + " order created";
            }
            if (order.ShipDate == DateTime.MinValue)
            {
                string? date_str = order.ShipDate.ToString("dd/MM/yyyy HH:mm:ss");
                return date_str + " order sent";
            }
            if (order.DeliveryDate == DateTime.MinValue)
            {
                string date_str = order.DeliveryDate.ToString("dd/MM/yyyy HH:mm:ss");
                return date_str + " order deliverd";
            }
            return " order created";


        }
        private static bool validID(int orderID)
        {
            return orderID > 0;
        }
        private static BO.Enums.Status OrderStatus(DO.Order thisOrder)
        {
            BO.Enums.Status status = BO.Enums.Status.confirmed;
            if (thisOrder.OrderDate != null)
            {
                status = BO.Enums.Status.confirmed;
            }
            if (thisOrder.ShipDate != null)
            {
                status = BO.Enums.Status.shipped;
            }
            if (thisOrder.DeliveryDate != null)
            {
                status = BO.Enums.Status.deliverd;
            }

            return status;
        }
        private static double GetTotalPrice(IEnumerable<DO.OrderItem?> OrdersItems)
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