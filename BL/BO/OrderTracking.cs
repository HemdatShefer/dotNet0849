using OtherFunctions;

namespace BO
{
    public class OrderTracking
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Enums.Status Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? OrderTrackingStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.toString();
        }
    }
}
