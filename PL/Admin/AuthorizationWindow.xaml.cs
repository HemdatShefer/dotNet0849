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
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();
        public AuthorizationWindow(BlApi.IBl bl)
        {
            InitializeComponent();
        }
        private void loginClick(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = MyPassword;
            if (passwordBox != null)
            {
                if (passwordBox.Password == "123456")
                {
                    new PL.Admin.AdminOperationWindow(bl!).Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
