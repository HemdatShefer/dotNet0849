using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
   public interface ICart
   {
            Cart AddOrderItem(Cart cart, int productId);
            void CommitCart(Cart cart);
            Cart UpdateOrderItem(Cart cart, int productId, int amount);
            void ClearItems(BO.Cart cart) => cart.Items.Clear();
   }
}