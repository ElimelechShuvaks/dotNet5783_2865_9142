using BO;
using DO;
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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for OrderItemWindow.xaml
    /// </summary>
    public partial class OrderItemWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.get();

     public BO.OrderItem Item { set; get; }

        public OrderItemWindow(BO.OrderItem orderItem)
        {
            Item = orderItem;
            InitializeComponent();
        }

        private void OrderItemWindowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.Order.OrderUpdate(Item.OrderId, Item.ProductId, Item.Amount);
                Close();
            }
            catch (BlExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
