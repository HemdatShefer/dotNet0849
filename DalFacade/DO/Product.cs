using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Product
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
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
        /// <returns></returns>
        public string Path { get; set; }

        public override string ToString() => $@"
    Product ID = {ID}: {Name}
    Category: {Categories}
    Price: {Price}
    Amount in stock: {InStock}";

    }
}
