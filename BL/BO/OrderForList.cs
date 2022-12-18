using OtherFunctions;

namespace BO
{
    public class OrderForList
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Enums.Status Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double ToalPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Amount { get; set; }

        public override string ToString()
        {
            return this.toString();
        }

    }
}
