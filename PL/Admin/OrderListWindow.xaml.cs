using PL.Product;
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
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    /// 


    public partial class OrderListWindow : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();
        public IEnumerable<BO.Order?> OrderList;
        public IEnumerable<BO.OrderForList?> OrderForList;

        public OrderListWindow(BlApi.IBl? bl)
        {
            InitializeComponent();
            this.bl = bl;
            OrderForList = bl!.Order.orderForLists();
            OrderListView.ItemsSource = OrderForList;
        }

        private void OrderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void OrderView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Order order= bl!.Order.GetOrder(((BO.OrderForList)OrderListView.SelectedItem).ID);
            new PL.Order.UpdateOrderWindow(order).Show();
        }

        private void OrderListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            BO.Order order = bl!.Order.GetOrder(((BO.OrderForList)OrderListView.SelectedItem).ID);
            new PL.Order.UpdateOrderWindow(order).Show();
        }

    }
}
