using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart;
        private int[] Amounts = new int[] { 1, 2, 3 ,4,5,6,7,8,9,10};

        public CartView(BlApi.IBl bl, BO.Cart _cart, BO.Order order)
        {
            this.bl = bl;
            cart = _cart;
            Loaded += CartView_Loaded;
            this.DataContext = cart;
            InitializeComponent();

            CustomerName.Text = cart.CustomerName;
            string s_total = string.Format("{0:0.00}", cart.TotalPrice);
            total.Text = s_total;
        }



        private void CartView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = cart;
        }

        private void commitCart_Click(object sender, RoutedEventArgs e)
        {
            bl!.Cart.CommitCart(cart);
            MessageBox.Show("Thanks for buying!");
            this.Close();
        }

        private void total_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomerName.Text = cart.CustomerName;
            string s_total = string.Format("{0:0.00}", cart.TotalPrice);
            total.Text = s_total;
        }
        private void CustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {

            CustomerName.Text = cart.CustomerName;
            string s_total = string.Format("{0:0.00}", cart.TotalPrice);
            total.Text = s_total;
        }

        private void itemsInCart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void itemsInCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((Selector)sender).SelectedItem as BO.OrderItem;
            var select = (BO.OrderItem)itemsInCart.SelectedItem;
           // new PL.Cart.UpdateAmountItems(bl!, cart, selectedItem.ProductID).Show();

            if (selectedItem is null)
            {
                MessageBox.Show("");
                return;
            }

        }

        private void itemsInCart_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show(((BO.OrderItem)itemsInCart.SelectedItem).ToString());
            this.Close();
        }
    }


}

