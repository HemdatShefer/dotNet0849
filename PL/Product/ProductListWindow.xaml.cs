using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductForList.xaml
    /// </summary>
    public partial class ProductForList : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();

        public IEnumerable<BO.ProductForList> ProductForLists;

        public ProductForList(BlApi.IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            ProductForLists = bl.Product.GetProductsForList();
            ProductListView.ItemsSource = ProductForLists;

            ProductSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void ProductSelector_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {
            ProductListView.ItemsSource = bl.Product.GetProductsForListByCond(ProductForLists, product => product.Categories == (BO.Enums.Category)ProductSelector.SelectedItem);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductWindowForOperations productWindowForOperations = new ProductWindowForOperations(bl);
            productWindowForOperations.ShowDialog();
            ProductForLists = bl.Product.GetProductsForList();
            ProductListView.ItemsSource = ProductForLists;

        }

        private void ProductListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //open operations window in add mode
            if (IsMouseCaptureWithin)
            {
                new ProductWindowForOperations(bl, ((BO.ProductForList)ProductListView.SelectedItem).ID).ShowDialog();
                ProductForLists = bl.Product.GetProductsForList();
                ProductListView.ItemsSource = ProductForLists;
            }

        }
    }
}
