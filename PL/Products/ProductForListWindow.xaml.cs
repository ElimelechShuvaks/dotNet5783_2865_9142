using BlApi;
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

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductForListWindow.xaml
/// </summary>
public partial class ProductForListWindow : Window
{
    IBl bl = new Bl();
    //IEnumerable<ProductForList> productForLists;
    
    public ProductForListWindow()
    {
        InitializeComponent();

        categorySelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));

        ProductListView.ItemsSource = bl.Product.ProductListRequest();

        //productForLists = bl.Product.ProductListRequest().Select(productForList => new ProductForList
        //{
        //    ProductForListBo = productForList,
        //    ImageSource = new BitmapImage(productForList.Uri)
        //});
        //ProductListView.ItemsSource = productForLists;
    }

    private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ProductListView.ItemsSource = bl.Product.ProductListRequest(productForLists => productForLists!.Category == (BO.Categories) categorySelector.SelectedItem);
    }

    private void addProduct(object sender, RoutedEventArgs e)
    {
        new ProductWindow(bl, ActionCase.Add).Show();
    }
}

//public class ProductForList
//{
//    public BO.ProductForList ProductForListBo { set; get; }

//    public ImageSource ImageSource { get; set; }

//}
