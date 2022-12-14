using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation
{
    public class Bl : IBl
    {
        public ICart cart => new Cart();
        public IProduct Product => new Product();
        public IOrder Order => new Order();

        IProduct IBl.Product { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IOrder IBl.Order { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        ICart IBl.Cart { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
