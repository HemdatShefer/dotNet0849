using BlImplementation;
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

namespace PL.Order
{

    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();
        public IEnumerable<BO.Order?> OrderList;
        public IEnumerable<BO.OrderForList?> OrderForList;
        public BO.Order orderToTrack;
        public OrderTracking(BlApi.IBl bl)
        {
            InitializeComponent();
            OrderForList = bl!.Order.orderForLists();
            OrderID.ItemsSource = OrderForList;
            this.DataContext = this;
        }

        public void SetSelectedOrderID(int value)
        {
            BO.OrderForList order;
            BO.OrderTracking tracking = bl.Order.GetOrderTracking(value);
            _orderTracking.Text = bl.Order.GetOrderTracking(value).OrderTrackingStatus; 
            BO.Order OrderTracking = bl.Order.GetOrder(value);
            orderToTrack = OrderTracking;
        }

        private void _orderTracking_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void OrderID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProductID = (sender as ComboBox).SelectedValue;
            if (selectedProductID != null)
            {
                var tracking = bl.Order.GetOrderTracking((int)(selectedProductID)); 
                _orderTracking.Text = tracking.OrderTrackingStatus;
            }
        }
    }

}
