using BO;
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

        private void OrderView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptureWithin)
            {
                BO.Order order = bl!.Order.GetOrder(((BO.OrderForList)OrderListView.SelectedItem).ID);
                BO.Enums.Status status = bl.Order.GetOrderTracking(order.ID).Status;
                if (status == BO.Enums.Status.shipped)
                {
                    new PL.Order.UpdateOrderWindow(bl, Visibility.Collapsed, Visibility.Visible, order).ShowDialog();
                }
                if (status == BO.Enums.Status.confirmed)
                {
                    new PL.Order.UpdateOrderWindow(bl, Visibility.Visible, Visibility.Collapsed, order).ShowDialog();
                }
                OrderForList = bl!.Order.orderForLists();
                OrderListView.ItemsSource = OrderForList;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item in the ListView
            var selectedItem = OrderListView.SelectedItem as BO.OrderForList;

            // Retrieve the order from the BL
            BO.Order order = bl!.Order.GetOrder(selectedItem.ID);
            BO.Enums.Status status = bl.Order.GetOrderTracking(order.ID).Status;

            // Check the order's status and open the appropriate window
            if (status == BO.Enums.Status.shipped)
            {
                new PL.Order.UpdateOrderWindow(bl, Visibility.Collapsed, Visibility.Visible, order).ShowDialog();
            }
            else if (status == BO.Enums.Status.confirmed)
            {
                new PL.Order.UpdateOrderWindow(bl, Visibility.Visible, Visibility.Collapsed, order).ShowDialog();
            }

            // Refresh the ListView
            OrderForList = bl!.Order.orderForLists();
            OrderListView.ItemsSource = OrderForList;
        }

    }
}
