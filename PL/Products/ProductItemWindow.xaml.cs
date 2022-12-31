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

    public ProductItemWindow(ProductItem sender, Cart senderCart, Action senderActio)
    {
        ProductItem = sender;
        Cart = senderCart;
        Action = senderActio;

        InitializeComponent();
    }

    private void AddCartButton_Click(object sender, RoutedEventArgs e)
    {
        bl.Cart.AddToCart(Cart, int.Parse(idTextBlock.Text));
        Action();
        this.Close();
    }
}
