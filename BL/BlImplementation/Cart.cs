using BlApi;
using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DO;
using BO;

namespace BlImplementation
{

    public class Cart : ICart
    {
        private IDal _dal = new DalList();
        BO.Cart ICart.AddOrderItem(BO.Cart cart, int productId)
        {
            try
            {
                DO.Product DOproduct = Dal.DalProduct.GetById(productId);
                BO.Product product = new BO.Product { ID = DOproduct.ID, Name = DOproduct.Name, Price = DOproduct.Price, InStock = DOproduct.InStock };
                BO.OrderItem orderItem = new BO.OrderItem { ID = DOproduct.ID, OrderID = cart.ID , Price = DOproduct.Price, Amount =  1, ProductID = productId };
                if (product.InStock <= 0)
                {
                    throw new NotInStockException("stock is empty");
                }

                foreach (var item in cart.Items.Where(item => item.ID == productId)){ item.Amount++; return cart; }
                cart.Items.Add(orderItem);
            }
            catch
            {
                throw new unValidException("id not valid");
            }
            return cart;
        }
        public BO.Cart UpdateOrderItem(BO.Cart cart, int productId, int amount)
        {
            try
            {
                DO.Product DOproduct = Dal.DalProduct.GetById(productId);
                BO.Product product = new BO.Product { ID = DOproduct.ID, Name = DOproduct.Name, Price = DOproduct.Price, InStock = DOproduct.InStock };
                BO.OrderItem orderItem = new BO.OrderItem { ID = DOproduct.ID, OrderID = cart.ID, Price = DOproduct.Price, Amount = amount, ProductID = productId };
                
                if (product.InStock- amount <=0 )
                {
                    throw new NotInStockException("Not enoge");
                }
                if (amount == 0)
                {
                    foreach (var item in cart.Items.Where(item => item.ProductID == productId)) { cart.Items.Remove(item); }
                }
                else
                {
                    foreach (BO.OrderItem item in cart.Items.Where(item => item.ProductID == productId)) { item.Amount = amount; }
                    cart.Items.Add(orderItem);
                }
            }
            catch
            {
                throw new unValidException("id not valid");
            }
            return cart;
        }
        public void CommitCart(BO.Cart cart)
        {
            try
            {
                CheckClient(cart);
                foreach(BO.OrderItem orderItem in cart.Items)
                {
                    CheckProduct(orderItem);
                }

            }
            catch
            {
                ////////
            }
            DO.Order order = new DO.Order() {ID = 0,OrderDate = DateTime.Now, DeliveryDate = DateTime.MinValue, ShipDate = DateTime.MinValue, CustomerAddress = "", CustomerEmail = "", CustomerName ="" };


        }
        private static void CheckClient(BO.Cart cart)
        {
            if(cart.ID <= 0)
            {
                throw new IDNotAdmissibleException("invalid ID");
            }
            if (cart.CustomerEmail == "")
            {
                throw new EmailUnvalidException("invalid Customer Email");
            }
            if(cart.CustomerName == "")
            {
                throw new NameUnvalidException("invalid Name");

            }
            if (cart.CustomerAdress =="")
            {
                throw new AdressUnvalidException("invalid Customer Adress");

            }
            if (cart.Total<0)
            {
                throw new TotalUnvalidException("invalid total price");
            }
        }

        private static void CheckProduct(BO.OrderItem product)
        {
            if (product.ID <= 0 || product.ID.ToString().Length < 5)
            {
                throw new IDNotAdmissibleException("invalid ID input");
            }
            if (product.OrderID <=0)
            {
                throw new NameNotAdmissibleException("invalid name input");
            }
            if (product.Price <= 0)
            {
                throw new PriceNotAdmissibleException("invalid price input");
            }
            if (product.ProductID < 0)
            {
                throw new StockNotAdmissibleException("invalid stock input");
            }
        }
    }

  
}

