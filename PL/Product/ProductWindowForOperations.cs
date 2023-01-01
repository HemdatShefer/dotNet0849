using BlApi;
using BO;
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
using System.Xaml;

namespace PL.Product
{

    public partial class ProductWindowForOperations : Window
    {
        public BlApi.IBl? bl1 = BlApi.Factory.Get();
        BO.Product? productToAdd;

        public ProductWindowForOperations(BO.ProductForList? prod = null)
        {
            InitializeComponent();
            if (prod is not null)
            {
                Display_Update(prod);
            }
            //        prod.Categories = Enum.GetValues(typeof(BO.Enums));
        }

        private void AddProductClick(object sender, RoutedEventArgs e)
        {
            try
            {
                productToAdd = new BO.Product
                {
                };


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
                BO.Product prod_to_update = new()
                {
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }

        }



        private void ProductNameChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ProductPriceChanged(object sender, TextChangedEventArgs e)
        {
        
 
        }

        private void ProductCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ProductAmountInStock_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void Display_Update(BO.ProductForList prod)
        { 
        }

    }
}
