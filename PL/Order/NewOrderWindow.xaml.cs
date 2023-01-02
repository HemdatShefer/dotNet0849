using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PL.Product;
using System.Windows.Input;

namespace PL.Order
{

    public partial class NewOrderWindow : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();
        public IEnumerable<BO.ProductForList> ProductForLists;
        public NewOrderWindow(BlApi.IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            ProductList.ItemsSource = bl.Product.GetProductsForList();
            ProductSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void ProductSelector_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {
            ProductList.ItemsSource = bl.Product.GetProductsForListByCond(ProductForLists, product => product.Categories == (BO.Enums.Category)ProductSelector.SelectedItem);
        }

        private void ProductListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //open operations window in add mode
            if (IsMouseCaptureWithin)
            {
                new ProductWindowForOperations(bl, ((BO.ProductForList)ProductList.SelectedItem).ID).ShowDialog();
                ProductForLists = bl.Product.GetProductsForList();
                ProductList.ItemsSource = ProductForLists;
            }

        }
    }
}
