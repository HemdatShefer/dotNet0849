using BlApi;
using BlImplementation;
using BO;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for AddToCartWindow.xaml
    /// </summary>
    public partial class AddToCartWindow : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Product Product { set; get; }
        public BO.Cart cart { set; get; }
        public IEnumerable<BO.ProductForList> Products;

        public IEnumerable<BO.Enums.Category> Categories { get; set; }

        public AddToCartWindow(BlApi.IBl bl)
        {
            Categories = Enum.GetValues(typeof(BO.Enums.Category)).Cast<BO.Enums.Category>();
            this.bl = bl;
            InitializeComponent();
            this.bl = bl;
            Products = bl.Product.GetProductsForList();
            ProductListView.ItemsSource = Products;
        }
    
        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = (BO.ProductForList)ProductListView.SelectedItem;
            if (selectedProduct != null)
            {
                bl.Cart.AddOrderItem(cart, selectedProduct.ID);
            }
        }
        private void AddProductClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.AddProduct(Product);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void UpdateProductClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.UpdateProduct(Product);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
    }
}


