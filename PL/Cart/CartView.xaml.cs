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
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart;

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
        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void total_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomerName.Text = cart.CustomerName;
            string s_total = string.Format("{0:0.00}", cart.TotalPrice);
            total.Text = s_total;
        }

        private void Adress_TextChanged(object sender, TextChangedEventArgs e)
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
    }


}

