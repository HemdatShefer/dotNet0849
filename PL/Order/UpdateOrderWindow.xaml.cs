using BlApi;
using Dal;
using Microsoft.VisualBasic;
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
        public UpdateOrderWindow(BlApi.IBl bl, Visibility Shipped, Visibility Deliverd, BO.Order _order)
        {
            this.bl = bl;
            order = _order;
            InitializeComponent();
            UpdateToDeliverd.Visibility = Deliverd;
            UpdateToShipped.Visibility = Shipped;
        }

        public UpdateOrderWindow(IBl bl, BO.Order order) : this(bl, Visibility.Collapsed, Visibility.Visible, order)
        {

        }

        private void OrderStatus_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        private void UpdateToDeliverd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.UpdateDeliverdDate(order.ID);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void UpdateToShipped_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.UpdateDeliverdDate(order.ID);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
    }
}
