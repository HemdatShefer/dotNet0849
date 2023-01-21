using BlApi;
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

namespace Admin
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        private IBl bl;
        public BO.Order Order { set; get; }
        public IEnumerable<BO.Enums.Category> Categories { get; set; }

        public OrderTrackingWindow(IBl bl, BO.Order order)
        {
            this.bl = bl;
            Order = order;
            InitializeComponent();
        }

        private void customerEmailTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
