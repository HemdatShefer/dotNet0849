using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DO;
using IProduct = BlApi.IProduct;

namespace BlImplementation
{

    internal class Product : IProduct
    {
        public void AddProduct(BO.Product product)
        {
            CheckProduct(product);
            try
            {
                Dal.DalProduct.Add(new DO.Product { ID = product.ID, Name = product.Name, Price = product.Price, InStock = product.InStock });
            }
            catch (DO.ObjectNotFoundException)
            {
                throw new unValidProductException("Product not valid");
            }
        }
        public void DeleteProduct(int idProduct)
        {
            IEnumerable<DO.Order?> orderList = Dal.DalOrder.GetAll();
            foreach (var _ in orderList.Where(item => item.Value.ID == idProduct).Select(item => new { }))
            {
                throw new DataException("product is in order");
            }
            try
            {
                Dal.DalProduct.Delete(idProduct);
            }
            catch
            {
                throw new unValidProductException("Product not valid");
            }
        }
        void IProduct.UpdateProduct(BO.Product product)
        {
            try
            {
               CheckProduct(product);
               Dal.DalProduct.update(new DO.Product { ID = product.ID, Name = product.Name, Price = product.Price, InStock = product.InStock });
            }
            catch 
            {
                throw new unValidProductException("Product not valid");

            }
        }
        BO.Product IProduct.GetProduct(int id)
        {
            DO.Product DOproduct = Dal.DalProduct.GetById(id);
            BO.Product product = new BO.Product { ID = DOproduct.ID, Name = DOproduct.Name, Price = DOproduct.Price, InStock = DOproduct.InStock };
            return product;
        }
        IEnumerable<ProductForList> IProduct.GetAllProductsForList()
        {
            List<BO.ProductForList> ProductForList = new List<BO.ProductForList>();
            IEnumerable<DO.Product?> productsList = Dal.DalProduct.GetAll();
            return (IEnumerable<BO.ProductForList>)(from product in ProductForList select product).ToList();
        }
        IEnumerable<BO.ProductForList> GetProductsForList(Func<ProductForList?, bool>? filter)
        {
            List<BO.ProductForList> ProductForList = new List<BO.ProductForList>();
            IEnumerable<DO.Product?> productsList = Dal.DalProduct.GetAll();
            return (IEnumerable<BO.ProductForList>)(from prod in ProductForList where filter(prod) select prod).ToList();

        }
        private static void CheckProduct(BO.Product product)
        {
            if (product.ID <= 0 || product.ID.ToString().Length < 5)
            {
                throw new IDNotAdmissibleException("invalid ID input");
            }
            if (product.Name == "")
            {
                throw new NameNotAdmissibleException("invalid name input");
            }
            if (product.Price <= 0)
            {
                throw new PriceNotAdmissibleException("invalid price input");
            }
            if (product.InStock < 0)
            {
                throw new StockNotAdmissibleException("invalid stock input");
            }
        }
    }


}