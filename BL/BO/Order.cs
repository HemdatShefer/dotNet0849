using OtherFunctions;

namespace BO
{
    public class Order
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? CustomerAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Enums.Status Status { get; set; }
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
        public double TotalPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderItem>? Items { get; set; }

        public override string ToString()
        {
            return this.toString();
        }
    }
}
