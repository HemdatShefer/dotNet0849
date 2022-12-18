using OtherFunctions;

namespace BO
{
    public class Cart
    {
        /// <summary>
        /// 
        /// </summary>
        public List<OrderItem>? Items { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? CustomerAdress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double TotalPrice { get; set; }

        public override string ToString()
        {
            return this.toString();
        }
    }
}
