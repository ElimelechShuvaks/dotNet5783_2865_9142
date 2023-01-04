using BlApi;
using BO;
using DO;
using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class OrderWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        bool DontShow;
        private BlApi.IBl? bl = BlApi.Factory.get();

        private BO.Order order;
        public BO.Order Order
        {
            get => order;
            set { order = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Order")); }
        }
        Action<BO.OrderStatus?> action;

        public OrderWindow(BO.Order senderOrder, bool Show, Action<BO.OrderStatus?> senderAction = null)
        {
            Order = senderOrder;
            DontShow = Show;
            action = senderAction;

            InitializeComponent();

            if (DontShow == true)
            {

                if (Order.Status == OrderStatus.Deliveried)
                {
                    ShipDateButton.Visibility = Visibility.Hidden;
                    DeliveryDateButton.Visibility = Visibility.Hidden;
                }
                else if (Order.Status == OrderStatus.Shipied)
                    ShipDateButton.Visibility = Visibility.Hidden;

                else
                    DeliveryDateButton.Visibility = Visibility.Hidden;

            }
            else
            {
                ShipDateButton.Visibility = Visibility.Hidden;
                DeliveryDateButton.Visibility = Visibility.Hidden;
            }
        }

        private void ShipDateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order = bl!.Order.OrderShippingUpdate(Order.Id);
                ShipDateButton.Visibility = Visibility.Hidden;
                DeliveryDateButton.Visibility = Visibility.Visible;
                action(Order.Status);
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
                Order = bl!.Order.OrderDeliveryUpdate(Order.Id);
                DeliveryDateButton.Visibility = Visibility.Hidden;
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
            }
        }
    }
}
