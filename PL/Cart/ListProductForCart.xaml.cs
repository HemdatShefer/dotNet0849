using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for ListProductForCart.xaml
    /// </summary>
    public partial class ListProductForCart : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();

        public IEnumerable<BO.ProductItem> ProductForLists;
        public BO.Cart cart;
        public ListProductForCart(BlApi.IBl bl, BO.Cart _cart)
        {
            InitializeComponent();
            this.DataContext = this;

            this.bl = bl;
            cart = _cart;

            ProductForLists = bl.Product.GetProductItems(cart );
            ProductListView.ItemsSource = ProductForLists;
            ProductListView.DataContext = ProductForLists;

            ProductSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void ProductSelector_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {
            ProductListView.ItemsSource = bl.Product.GetProductItems(cart, product => product.Categories == (BO.Enums.Category)ProductSelector.SelectedItem);
        }

        private void amountSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            int productId = (int)((ListViewItem)comboBox.Parent).DataContext;
            int amount = int.Parse(((ComboBoxItem)comboBox.SelectedItem).Content.ToString()!);
            cart = bl!.Cart.UpdateOrderItem(cart, productId, amount);
        }


        private void Cart_Click(object sender, RoutedEventArgs e) => new PL.Cart.NewCostumerCart(bl, cart).Show(); 


        private void ProductListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = ((Selector)sender).SelectedItem as BO.ProductItem;
            var select = (BO.ProductItem)ProductListView.SelectedItem;
            if (selectedProduct is null)
            {
                MessageBox.Show("Please select a product to add to cart.");
                return;
            }
            
            try
            {
                if (selectedProduct is not null)
                {
                    bl!.Cart.AddOrderItem(cart, selectedProduct.ID);
                    MessageBox.Show("product added to cart");

                }

            }
            catch (ObjectNotFoundException ex)
            {
                MessageBox.Show("Product not found.");
            }
            catch (BlImplementation.NotInStockException ex)
            {
                MessageBox.Show("Not enough stock to add this product to the cart.");
                
            }

        }




    }


    }
