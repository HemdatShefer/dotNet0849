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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for UpdateOrderWindow.xaml
    /// </summary>
    public partial class UpdateOrderWindow : Window
    {

        public BlApi.IBl? bl = BlApi.Factory.Get();

        public IEnumerable<BO.ProductForList> ProductForLists;
        BO.Order order;
        public UpdateOrderWindow(BO.Order _order)
        {
            InitializeComponent();
            this.bl = bl;
            order = _order;
        }

        private void customerAddressTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerAddressTextBox1.Text = order.CustomerAddress;
        }

        private void customerEmailTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerEmailTextBox1.Text = order.CustomerEmail;
        }

        private void customerNameTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerNameTextBox1.Text = order.CustomerName;
        }

    }
}
