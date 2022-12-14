using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
public interface IProduct
{
    void AddProduct(Product product);
   void DeleteProduct(int idProduct);
    void UpdateProduct(Product product);
    Product GetProduct(int id);
    IEnumerable<BO.ProductForList?> GetAllProductsForList();

}
}