namespace DO;

public struct OrderItem
{
    /// <summary>
    /// 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int ProductID { get; set; }//
    /// <summary>
    /// 
    /// </summary>
    public int OrderID { get; set; }//
    /// <summary>
    /// 
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    Item ID = {ID}
    Product ID = {ProductID}
    Order ID = {OrderID}
    Price: {Price}
    Amount: {Amount}";
}