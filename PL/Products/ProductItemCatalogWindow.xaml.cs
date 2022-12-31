using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
/// Interaction logic for ProductCatalogWindow.xaml
/// </summary>
public partial class ProductCatalogWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    IBl bl = Factory.get();
    IEnumerable<IGrouping<BO.Categories?, ProductItem>> groups;

    //public ObservableCollection<ProductItem> ProductItems = new ObservableCollection<ProductItem>();
    private IEnumerable<BO.ProductItem?> productItems;
    public IEnumerable<BO.ProductItem?> ProductItems { 
        get => productItems;
        set { productItems = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ProductItems")); } }

    private Cart cart;
    public Cart Cart { get => cart; set { cart = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Cart")); } }

    Action action; 

    public ProductCatalogWindow()
    {
        Cart = new Cart { Items = new() };
        ProductItems = bl.Product.ProductListRequest().Select(item => bl.Product.ProductDetailsClient(Cart, item!.Id));

        InitializeComponent();

        //ProductItemsListView.ItemsSource = ProductItems;

        groups = from e in ProductItems
                 group e by e.Category into t
                 select t;

        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ProductItemsListView.DataContext = groups.FirstOrDefault(item => (BO.Categories)CategorySelector.SelectedItem == item.Key);
        refreshButton.Visibility = Visibility.Visible;
    }

    private void refreshButton_Click(object sender, RoutedEventArgs e)
    {
        ProductItemsListView.DataContext = ProductItems;
        refreshButton.Visibility = Visibility.Hidden;
    }

    private void ProductDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ProductItem item = ProductItemsListView.SelectedItem as ProductItem;
        action = () => ProductItems = ProductItems; 
        new ProductItemWindow(item!, Cart, action).ShowDialog();
    }

    private void cartButton_Click(object sender, RoutedEventArgs e)
    {
        new CartWindow(Cart).ShowDialog();
    }
}