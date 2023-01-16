using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void adminClick(object sender, RoutedEventArgs e) => new PL.Admin.AuthorizationWindow(bl!).Show();
        private void NewOrderClick(object sender, RoutedEventArgs e) => new PL.Cart.AddToCartWindow(bl!).Show();

        private void Button_Click(object sender, RoutedEventArgs e) => new PL.Order.OrderTracking(bl!).Show();
    }
}
