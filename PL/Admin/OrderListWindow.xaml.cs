using BO;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;


namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml.
    /// This window displays a list of orders and allows the user to update order statuses.
    /// </summary>
    public partial class OrderListWindow : Window
    {
        private readonly BlApi.IBl bl = BlApi.Factory.Get();
        private DispatcherTimer refreshTimer;

        /// <summary>
        /// ObservableCollection that is bound to the ListView in the XAML.
        /// This allows the UI to automatically update when items are added or removed.
        /// </summary>
        public ObservableCollection<OrderForList> Order_List { get; } = new ObservableCollection<OrderForList>();

        /// <summary>
        /// Constructor that initializes the window and sets up data and UI components.
        /// </summary>
        /// <param name="bl">The business logic layer instance.</param>
        public OrderListWindow(BlApi.IBl bl)
        {
            InitializeComponent();
            this.bl = bl ?? throw new ArgumentNullException(nameof(bl));
            this.DataContext = this;  // Set the DataContext for data binding.
            LoadOrdersAsync();  // Load initial set of orders.
            SetupRefreshTimer();  // Setup the timer to refresh orders periodically.
        }

        /// <summary>
        /// Sets up a timer to refresh the order list every 10 seconds.
        /// </summary>
        private void SetupRefreshTimer()
        {
            refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            refreshTimer.Tick += async (_, __) => await RefreshOrdersAsync();
            refreshTimer.Start();
        }


        private async void LoadOrdersAsync()
        {
            await Task.Run(() =>
            {
                var orders = bl.Order.orderForLists();
                Dispatcher.Invoke(() =>
                {
                    Order_List.Clear();
                    foreach (var order in orders)
                    {
                        Order_List.Add(order);
                    }
                });
            });
        }

        /// <summary>
        /// Refreshes the order list by re-querying from the business layer.
        /// </summary>
        private async Task RefreshOrdersAsync()
        {
            var updatedOrders = bl.Order.orderForLists();
            await Dispatcher.InvokeAsync(() =>
            {
                Order_List.Clear();
                foreach (var order in updatedOrders)
                {
                    Order_List.Add(order);
                }
            });
        }


        /// <summary>
        /// Event handler for the window being closed. Stops the refresh timer to clean up resources.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            refreshTimer?.Stop();
        }

        public void Dispose()
        {
            refreshTimer?.Stop();
            refreshTimer = null;
        }

        /// <summary>
        /// Event handler for the "Shipped" button click. Updates the shipped status of the order.
        /// </summary>
        private void UpdateShippedButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var order = (OrderForList)button.DataContext;
            var status = bl.Order.GetOrderTracking(order.ID).Status;

            if (status == BO.Enums.Status.deliverd || status == BO.Enums.Status.shipped)
            {
                MessageBox.Show("This order has already been shipped or delivered.");
            }
            else
            {
            bl.Order.UpdateshippedDate(order.ID);
            RefreshOrdersAsync();
            }
        }

        /// <summary>
        /// Event handler for the "Delivered" button click. Updates the delivery status of the order.
        /// </summary>
        private void UpdateDeliverdButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var order = (OrderForList)button.DataContext;
            var status = bl.Order.GetOrderTracking(order.ID).Status;

            if (status == BO.Enums.Status.deliverd)
            {
                MessageBox.Show("This order has already been delivered.");
            }
            else if (status == BO.Enums.Status.confirmed)
            {
                MessageBox.Show("This order has not been shipped yet.");
            }
            else
            {
                bl.Order.UpdateDeliverdDate(order.ID);
                RefreshOrdersAsync(); // Refresh the list to show the updated status.
            }
        }


        private void OrderView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}


