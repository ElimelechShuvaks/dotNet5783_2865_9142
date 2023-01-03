using BlApi;
using BO;
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

namespace PL.Products;

/// <summary>
/// Interaction logic for orderForListWindow.xaml
/// </summary>
public partial class orderForListWindow : Window
{
    public static readonly DependencyProperty OrderForListProperty = DependencyProperty.Register(nameof(OrderForList), typeof(IEnumerable<BO.OrderForList>), typeof(orderForListWindow));

    public IEnumerable<BO.OrderForList> OrderForList { get => (IEnumerable<BO.OrderForList>)GetValue(OrderForListProperty); set => SetValue(OrderForListProperty, value); }

    IBl bl = BlApi.Factory.get();

    public IEnumerable<BO.OrderStatistics> orderStatistics { get; set; }
    public orderForListWindow()
    {
        OrderForList = bl?.Order.OrderForListRequest()!;
        orderStatistics = bl?.Order.GetOrderStatiscs(OrderForList)!;
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
            OrderForList = bl?.Order.OrderForListRequest()!;
        else
            OrderForList = bl?.Order.OrderForListRequest().Where(orderStatus => orderStatus!.Status ==(OrderStatus)Selector.SelectedItem)!;
    }

    private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        new OrderWindow((BO.OrderForList)OrderListView.SelectedItem,true,this).ShowDialog();

    }

    private void OrderList(object sender, RoutedEventArgs e)
    {
        OrderForList= bl?.Order.OrderByName(OrderForList)!;
    }

    private void ShowOrder(object sender, RoutedEventArgs e)
    {
        orderStatistics = bl?.Order.GetOrderStatiscs(OrderForList)!;

    }
}