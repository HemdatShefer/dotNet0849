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

namespace PL.Cart
{

    public partial class NewCostumerCart : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart;
        public NewCostumerCart(BlApi.IBl bl, BO.Cart _cart)
        {
            this.bl = bl;
            cart = _cart;
            InitializeComponent();

        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomerName.Text) || string.IsNullOrEmpty(CustomerAddress.Text) || string.IsNullOrEmpty(CustomerEmail.Text))
            {
                MessageBox.Show("Please fill in all the required fields before checking out.");
            }
            else
            {
                cart.CustomerName = CustomerName.Text;
                cart.CustomerAdress = CustomerAddress.Text;
                cart.CustomerEmail = CustomerEmail.Text;
                BO.Order order = createNewOrder(cart);
                new PL.Cart.CartView(bl!, cart, order).Show();
            }
        }
        private static BO.Order createNewOrder(BO.Cart cart)
        {
            BO.Order order = new BO.Order()
            {
                CustomerAddress = cart.CustomerAdress,
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                Items = cart.Items,
                DeliveryDate = DateTime.MinValue,
                OrderDate = DateTime.MinValue,
                ShipDate = DateTime.MinValue,
                TotalPrice = 0,
                Status = BO.Enums.Status.confirmed
            };

            return order;
        }
    }
}
