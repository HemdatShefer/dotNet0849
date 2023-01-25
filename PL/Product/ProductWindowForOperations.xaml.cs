using BlApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;

using System.Xml.Linq;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductWindowForOperations.xaml
    /// </summary>
    public partial class ProductWindowForOperations : Window
    {
        private IBl bl;
        public BO.Product Product { set; get; }

        public IEnumerable<BO.Enums.Category> Categories { get; set; }

        public ProductWindowForOperations(IBl bl, Visibility add, Visibility update, BO.Product product)
        {
            Categories = Enum.GetValues(typeof(BO.Enums.Category)).Cast<BO.Enums.Category>();
            Product = product;
            this.bl = bl;

            InitializeComponent();
            AddProduct.Visibility = add;
            UpdateProduct.Visibility = update;
        }

        public ProductWindowForOperations(IBl bl) : this(bl, Visibility.Visible, Visibility.Collapsed, new BO.Product())
        {

        }

        public ProductWindowForOperations(IBl bl, int id) : this(bl, Visibility.Collapsed, Visibility.Visible, bl.Product.GetProduct(id))
        {

        }
      
        private void AddProductClick(object sender, RoutedEventArgs e)
        {
            //if(string.IsNullOrEmpty(ProductID.Text)  || string.IsNullOrEmpty(ProductName.Text) || string.IsNullOrEmpty(ProductPrice.Text) || string.IsNullOrEmpty(ProductAmountInStock.Text) )
            //{
            //}
            //if(double.TryParse(ProductPrice.Text, out price))
            //            Button button = (Button)sender;
          //  BO.OrderForList order = (BO.OrderForList)button.DataContext;
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
                MessageBox.Show("Update secuessfuly");
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
