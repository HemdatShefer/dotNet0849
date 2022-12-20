using BO;
using System.Data;
using IProduct = BlApi.IProduct;

namespace BlImplementation
{

    internal class Product : IProduct
    {
     private Dal.DalProduct _dal = new Dal.DalProduct();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="unValidProductException"></exception>
        public void AddProduct(BO.Product product)
        {
            checkProduct(product);
            try
            {
                _dal.Add(new DO.Product { ID = product.ID, Name = product.Name!, Price = product.Price, InStock = product.InStock });
            }
            catch (DO.ObjectNotFoundException)
            {
                throw new unValidProductException("Product not valid");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProduct"></param>
        /// <exception cref="DataException"></exception>
        /// <exception cref="unValidProductException"></exception>
        public void DeleteProduct(int idProduct)
        {
            IEnumerable<DO.Order> orderList = new Dal.DalOrder().GetAll();

            foreach (var _ in orderList.Where(item => item.ID == idProduct).Select(item => new { }))
            {
                throw new DataException("product is in order");
            }
            try
            {
                _dal.Delete(idProduct);
            }
            catch
            {
                throw new unValidProductException("Product not valid");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="unValidProductException"></exception>
        void IProduct.UpdateProduct(BO.Product product)
        {
            try
            {
                checkProduct(product);
                _dal.Update(new DO.Product { ID = product.ID, Name = product.Name!, Price = product.Price, InStock = product.InStock });
            }
            catch
            {
                throw new unValidProductException("Product not valid");

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BO.Product IProduct.GetProduct(int id)
        {
            DO.Product DOproduct = _dal.GetById(id);
            BO.Product product = new BO.Product { ID = DOproduct.ID, Name = DOproduct.Name, Price = DOproduct.Price, InStock = DOproduct.InStock, Categories = (BO.Enums.Category)DOproduct.Categories };
            return product;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<BO.ProductForList> GetProductsForList(Func<ProductForList?, bool>? filter = null)
        {
            IEnumerable<DO.Product> productsList = _dal.GetAll();

            return from prod in productsList
                   select new BO.ProductForList
                   {
                       ID = prod.ID,
                       Name = prod.Name,
                       Price = prod.Price,
                       InStock = prod.InStock,
                       Categories = (BO.Enums.Category)prod.Categories,
                       Path = prod.Path
                   };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="IDNotAdmissibleException"></exception>
        /// <exception cref="NameNotAdmissibleException"></exception>
        /// <exception cref="PriceNotAdmissibleException"></exception>
        /// <exception cref="StockNotAdmissibleException"></exception>
        private static void checkProduct(BO.Product product)
        {
            if (product.ID <= 0)
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