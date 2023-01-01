using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Globalization;
using System.Collections.ObjectModel;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductForList.xaml
    /// </summary>
    public partial class ProductForList : Window
    {
        public BlApi.IBl? bl1 = BlApi.Factory.Get();
        public delegate void EventHandler(object? sender, EventArgs e);
        public ProductForList(BlApi.IBl bl)
        {
          }

        private void ProductSelector_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //open operations window in add mode
            ProductWindowForOperations productWindowForOperations = new ProductWindowForOperations();
            productWindowForOperations.Show();
            this.Close();
            productWindowForOperations.UpdateProduct.Visibility = Visibility.Collapsed;
            productWindowForOperations.AddProduct.Visibility = Visibility.Visible;

        }

        private void ProductListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
   
        }
    }
}
