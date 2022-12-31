using BlApi;
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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        bool DontShow;
        private BlApi.IBl? bl = BlApi.Factory.get();

        orderForListWindow orderForListWindow;

        public BO.Order order { get; set; }

        public OrderWindow(BO.OrderForList orderForList, bool Show, orderForListWindow _orderForListWindow)
        {
            orderForListWindow = _orderForListWindow;
            order = bl.Order.GetDetailsOrder(orderForList.OrderId);

            InitializeComponent();

            DontShow = Show;

                if (order.ShipDate != null && order.DeliveryDate != null)
                {
                    ShipDateButton.Visibility = Visibility.Hidden;
                    DeliveryDateButton.Visibility = Visibility.Hidden;
                }
                if (order.ShipDate == null)
                    DeliveryDateButton.Visibility = Visibility.Hidden;

                if (order.DeliveryDate == null && order.ShipDate != null)
                    ShipDateButton.Visibility = Visibility.Hidden;
       
        }
        public OrderWindow(BO.OrderForList orderForList, bool Show)
        {
           
            order = bl.Order.GetDetailsOrder(orderForList.OrderId);

            InitializeComponent();

            DontShow = Show;

            ShipDateButton.Visibility = Visibility.Hidden;
            DeliveryDateButton.Visibility = Visibility.Hidden;
        }

        private void ShipDateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.Order.OrderShippingUpdate(order.Id);
                order = bl.Order.GetDetailsOrder(order.Id);
                ShipDateTextBox.Text = order.ShipDate.ToString();
                ShipDateButton.Visibility = Visibility.Hidden;
                DeliveryDateButton.Visibility = Visibility.Visible;
                orderForListWindow.OrderForList = bl?.Order.OrderForListRequest()!;
            }
            catch (BlExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeliveryDateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.Order.OrderDeliveryUpdate(order.Id);
                order = bl.Order.GetDetailsOrder(order.Id);
                DeliveryDateTextBox.Text = order.DeliveryDate.ToString();
                DeliveryDateButton.Visibility = Visibility.Hidden;
                orderForListWindow.OrderForList = bl?.Order.OrderForListRequest()!;
            }
            catch (BlExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpDataItem(object sender, MouseButtonEventArgs e)
        {
            if (DontShow)
            {
                new OrderItemWindow((BO.OrderItem)OrderItemListView.SelectedItem).ShowDialog();
                orderForListWindow.OrderForList = bl?.Order.OrderForListRequest()!;
            }

        }
    }
}
