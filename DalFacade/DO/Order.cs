namespace DO;
public struct Order
{
    /// <summary>
    /// 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string CustomerAddress { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime ShipDate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime DeliveryDate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>

    public override string ToString() => $@"
    Order Number = {ID}
    Customer Name - {CustomerName}
    Customer Email - {CustomerEmail}
    Customer Address - {CustomerAddress}
    Order Date - {OrderDate}
    Shipment Date - {ShipDate}
    Delivery Date - {DeliveryDate}";
}
