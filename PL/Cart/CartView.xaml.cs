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
            try 
            {
                bl!.Cart.CommitCart(cart);

                MessageBox.Show("Thanks for buying!");
            }
            catch(EmptyCartException ex)
            {
                MessageBox.Show(ex.Message);
            }

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
        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //var scrollBar = sender as ScrollBar;

            //var selectedItem = itemsInCart.SelectedItem as BO.OrderItem;
            //var productId = selectedItem.ProductID;
            //var newAmount = (int)scrollBar.Value;
            //Button button = (Button)sender;
            //BO.OrderItem item = (BO.OrderItem)scrollBar.DataContext;
            //bl.Cart.UpdateOrderItem(cart, productId, newAmount);

        }

        private void itemsInCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((Selector)sender).SelectedItem as BO.OrderItem;
            var select = (BO.OrderItem)itemsInCart.SelectedItem;

        }

        private void itemsInCart_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show(((BO.OrderItem)itemsInCart.SelectedItem).ToString());
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            var scrollBar = sender as ScrollBar;

            var newAmount = (int)scrollBar.Value ;


            ScrollBar ScrollBar = (ScrollBar)sender;
            BO.OrderItem item = (BO.OrderItem)scrollBar.DataContext;

            try
            {
                if (e.ScrollEventType == ScrollEventType.SmallDecrement)
                {
                    bl.Cart.UpdateOrderItem(cart, item.ProductID, newAmount + 1);
                }
                else if (e.ScrollEventType == ScrollEventType.SmallIncrement)
                {
                    bl.Cart.UpdateOrderItem(cart, item.ProductID, newAmount - 1);
                }
            }
            catch(NotInStockException ex)
            { 
                MessageBox.Show(ex.Message);
            }

            Loaded += CartView_Loaded;
            ((CollectionView)CollectionViewSource.GetDefaultView(itemsInCart.ItemsSource)).Refresh();
            this.DataContext = cart;

            string s_total = string.Format("{0:0.00}", cart.TotalPrice);
            total.Text = s_total;

            var binding = total.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
            ((CollectionView)CollectionViewSource.GetDefaultView(itemsInCart.ItemsSource)).Refresh();
            BindingOperations.GetBindingExpression(total, TextBox.TextProperty)?.UpdateTarget();
        }
    }


}

