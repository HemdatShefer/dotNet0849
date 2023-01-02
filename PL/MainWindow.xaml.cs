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
        private void MainClick(object sender, RoutedEventArgs e) => new PL.Product.ProductForList(bl!).Show();
        private void NewOrderClick(object sender, RoutedEventArgs e) => new PL.Order.NewOrderWindow(bl!).Show();
    }
}
