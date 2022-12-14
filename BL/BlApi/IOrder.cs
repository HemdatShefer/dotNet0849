using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlImplementation;
using BO;

namespace BlApi
{

    public interface IOrder
    {
        IEnumerable<BO.OrderForList?> orderForLists();
        BO.Order GetOrderManger(int orderID);
        BO.Order GetOrderClient(int orderID);
        BO.Order UpdateDelivery(int orderID);
        BO.Order UpdatSupplay(int orderID);
        BO.OrderTracking GetOrderTracking(int orderID);
    }
}