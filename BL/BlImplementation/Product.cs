using BO;
using System.Data;
using IProduct = BlApi.IProduct;

namespace BlImplementation
{

    internal class Product : IProduct
    {
        DalApi.IDal? _dal = DalApi.Factory.Get();

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
                _dal.Product.Add(new DO.Product { ID = product.ID, Name = product.Name!, Price = product.Price, InStock = product.InStock, 
                    Path = product.Path == null? "D:\\repos\\dotNet0849\\photos\\MissingImage.png" : product.Path, Categories = (DO.Enums.Category)product.Categories
                });
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
                _dal.Product.Delete(idProduct);
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
                _dal.Product.Update(new DO.Product { ID = product.ID, Name = product.Name!, Price = product.Price, InStock = product.InStock, 
                    Categories = (DO.Enums.Category)product.Categories, Path = product.Path == null ? "D:\\repos\\dotNet0849\\photos\\MissingImage.png" : product.Path
                });
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
            DO.Product DOproduct = _dal.Product.GetById(id);
            BO.Product product = new BO.Product { ID = DOproduct.ID, Name = DOproduct.Name, Price = DOproduct.Price, InStock = DOproduct.InStock, Categories = (BO.Enums.Category)DOproduct.Categories , Path = DOproduct.Path};
            return product;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<BO.ProductForList> GetProductsForList(Func<ProductForList?, bool>? filter = null)
        {
            IEnumerable<DO.Product> productsList = _dal.Product.GetAll();

            return from prod in productsList
                   select new ProductForList
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

        public IEnumerable<ProductForList> GetProductsForListByCond(IEnumerable<ProductForList> productForLists, Func<ProductForList?, bool>? filter)
        {
            return productForLists.Where(filter);
        }

        public ProductItem GetProductItem(int productId, BO.Cart cart)
        {
            DO.Product prod = _dal!.Product.GetById(productId);  
           return new ProductItem
            {
                ID = prod.ID,
                Name = prod.Name,
                Price = prod.Price,
                InStock = prod.InStock > 0,
                Categories = (BO.Enums.Category)prod.Categories,
                Path = prod.Path,
                AmountInCart = (from item in cart.Items
                               where item.ProductID == prod.ID
                               select item.Amount).FirstOrDefault(0)
           };
        }

        public IEnumerable<ProductItem> GetProductItems(BO.Cart cart, Func<ProductItem?, bool>? filter = null)
        {
            IEnumerable<DO.Product> productsList = _dal.Product.GetAll();
            //, Func<ProductItem?, bool>? filter
            return from prod in productsList
                   let productItem = GetProductItem(prod.ID, cart)
                   where filter is null? true : filter(productItem)
                   select productItem;
        }
    }


}