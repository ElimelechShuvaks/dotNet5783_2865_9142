using BlApi;
using BO;
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

namespace PL.Products;

/// <summary>
/// Interaction logic for orderForListWindow.xaml
/// </summary>
public partial class orderForListWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private List<OrderForList> orderForLists;
    public List<BO.OrderForList> OrderForLists
    {
        get => orderForLists;
        set { orderForLists = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("OrderForLists")); }
    }

    public IEnumerable<BO.OrderStatistics> OrderStatistics { get; set; }

    Action<BO.OrderStatus?> action;

    IBl bl = BlApi.Factory.get();

    public orderForListWindow()
    {
        OrderForLists = bl?.Order.OrderForListRequest()!.ToList()!;

        OrderStatistics = bl?.Order.GetOrderStatiscs(OrderForLists)!;

        InitializeComponent();

        for (int i = 0; i < 3; i++) // include the 3 OrderStatus into the comboBox
        {
            Selector.Items.Add((BO.OrderStatus)i);
        }
        Selector.Items.Add("All orders"); // add a basic condition.
    }

    private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Selector.SelectedItem.ToString() == "All orders")
            OrderForLists = bl?.Order.OrderForListRequest()!.ToList()!;
        else
            OrderForLists = bl?.Order.OrderForListRequest().Where(orderStatus => orderStatus!.Status == (OrderStatus)Selector.SelectedItem)!.ToList()!;
    }

    private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.OrderForList orderForList = (OrderListView.SelectedItem as BO.OrderForList)!;
        BO.Order order = bl.Order.GetDetailsOrder(orderForList.OrderId);

        action = (BO.OrderStatus? status) =>
        {
            orderForList.Status = status;
            OrderForLists = OrderForLists.Select(item => item).ToList();
        };
        new OrderWindow(order, true, action).ShowDialog();
    }

    private void OrderList(object sender, RoutedEventArgs e)
    {
        OrderForLists = bl?.Order.OrderByName(OrderForLists)!.ToList()!;
    }

    private void ShowOrder(object sender, RoutedEventArgs e)
    {
        OrderStatistics = bl?.Order.GetOrderStatiscs(OrderForLists)!;
    }
}