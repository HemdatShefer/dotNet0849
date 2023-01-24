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

        private void UpdateShippedButtonClick(object sender, RoutedEventArgs e)
        {
            // Get the selected item in the ListView            
            Button button = (Button)sender;
            BO.OrderForList order = (BO.OrderForList)button.DataContext;
            BO.Enums.Status status = bl.Order.GetOrderTracking(order.ID).Status;
            if (status == BO.Enums.Status.deliverd || status == BO.Enums.Status.shipped)
            {
                MessageBox.Show("This Order Alredy been shipped");
            }
            else
            {
                bl.Order.UpdateshippedDate(order.ID);
            }          

            // Refresh the ListView
            OrderForList = bl!.Order.orderForLists();
            OrderListView.ItemsSource = OrderForList;
        }

        private void UpdateDeliverdButtonClick(object sender, RoutedEventArgs e)
        {
            // Get the selected item in the ListView            
            Button button = (Button)sender;
            BO.OrderForList order = (BO.OrderForList)button.DataContext;
            BO.Enums.Status status = bl.Order.GetOrderTracking(order.ID).Status;

            if (status == BO.Enums.Status.deliverd)
            {
                MessageBox.Show("This Order Alredy been deliverd");
            }
            else if (status == BO.Enums.Status.confirmed)
            {
                MessageBox.Show("This Order haven't shipped yet");
            }
            else
            {
                bl.Order.UpdateDeliverdDate(order.ID);
            }


            // Refresh the ListView
            OrderForList = bl!.Order.orderForLists();
            OrderListView.ItemsSource = OrderForList;
        }
    }
}
