using BlImplementation;
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

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for AdminOperationWindow.xaml
    /// </summary>
    public partial class AdminOperationWindow : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();

        public AdminOperationWindow(BlApi.IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void productListClick(object sender, RoutedEventArgs e) => new PL.Product.ProductForList(bl!).Show();
        private void orderListClick(object sender, RoutedEventArgs e) => new PL.Admin.OrderListWindow(bl!).Show();

    }
}
