using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{ 
    public interface IBl
    {
        public IProduct Product { get; set; }
        public IOrder Order { get; set; }
        public ICart Cart { get; set; }

    }
}
