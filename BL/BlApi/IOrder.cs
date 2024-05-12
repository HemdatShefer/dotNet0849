namespace BlApi;


public interface IOrder
{
    IEnumerable<BO.OrderForList?> orderForLists();
    BO.Order GetOrder(int orderID);
    BO.Order UpdateshippedDate(int orderID);
    BO.Order UpdateDeliverdDate(int orderID);
    BO.OrderTracking GetOrderTracking(int orderID);
    BO.Order OldOrder();
    bool CheckIfAllOrdersFinished();
}