using BlApi;
using OtherFunctions;

namespace BO
{
    public class ProductForList
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
        public Enums.Category Categories { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int InStock { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Path { get; set; }

        public override string ToString()
        {
            return this.toString();
        }

    }
}
