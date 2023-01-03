using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
/// Interaction logic for ProductItemWindow.xaml
/// </summary>
public partial class ProductItemWindow : Window
{
    IBl bl = Factory.get();

    public ProductItem ProductItem { get; set; }
    public Cart Cart { get; set; }
    Action Action { get; set; }

    public ProductItemWindow(ProductItem senderItem, Cart senderCart, Action senderActio)
    {
        ProductItem = senderItem;
        Cart = senderCart;
        Action = senderActio;

        InitializeComponent();
    }

    private void AddCartButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Cart.AddToCart(Cart, ProductItem.Id);
            Action();
            Thread.Sleep(500);
            Close();
        }
        catch (BlExceptions ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void UpdateCartButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Cart.ProductUpdateCart(Cart, ProductItem.Id, int.Parse(textNumber.Text));
            Action();
            Close();
        }
        catch (BlExceptions ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void cmdUp_Click(object sender, RoutedEventArgs e)
    {
        textNumber.Text = (int.Parse(textNumber.Text) + 1).ToString();
    }

    private void cmdDown_Click(object sender, RoutedEventArgs e)
    {
        int num = int.Parse(textNumber.Text);
        textNumber.Text = (num > 0 ? num -= 1 : num = 0).ToString();
    }
}
