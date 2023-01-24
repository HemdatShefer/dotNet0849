using BlApi;
using Dal;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for UpdateAmountItems.xaml
    /// </summary>
    public partial class UpdateAmountItems : Window
    {
        public int itemID { set; get; }

        public BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart;


        public UpdateAmountItems(BlApi.IBl bl, BO.Cart _cart, int _itemID)
        {
            this.bl = bl;
            cart = _cart;
            itemID = _itemID;
            InitializeComponent();
        }
        private void Amount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedValue = (sender as ComboBox).SelectedValue.ToString();
            int newAmount = int.Parse(selectedValue);
            try
            {
                bl.Cart.UpdateOrderItem(cart, itemID, newAmount);
            }
            catch( Exception ex) 
            {

            }
        }

    }
}
