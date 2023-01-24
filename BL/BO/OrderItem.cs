using OtherFunctions;

namespace BO
{
    public class OrderItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OrderID { get; set; }
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
        public double Total { get; set; }

        public override string ToString()
        {
            return this.toString();
        }
    }
}
