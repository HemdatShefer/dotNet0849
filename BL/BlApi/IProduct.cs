using BO;

namespace BlApi
{
    public interface IProduct
    {
        void AddProduct(Product product);
        void DeleteProduct(int idProduct);
        void UpdateProduct(Product product);
        Product GetProduct(int id);
        IEnumerable<BO.ProductForList> GetProductsForListByCond(IEnumerable<BO.ProductForList> productForLists, Func<ProductForList?, bool>? filter);
        IEnumerable<BO.ProductForList> GetProductsForList(Func<ProductForList?, bool>? filter = null);
        ProductItem GetProductItem(int productId, Cart cart);
        IEnumerable<ProductItem> GetProductItems(BO.Cart cart, Func<ProductItem?, bool>? filter = null);

    }
}