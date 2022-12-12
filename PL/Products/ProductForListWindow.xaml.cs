using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    IBl bl = Factory.get();

    public ProductForListWindow()
    {
        InitializeComponent();

        for (int i = 0; i < 5; i++) // include the 5 categories into the comboBox
        {
            categorySelector.Items.Add((BO.Categories)i);
        }
        categorySelector.Items.Add("All products"); // add a basic condition.

        ProductListView.ItemsSource = bl.Product.ProductListRequest();

    }

    private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (categorySelector.SelectedItem.ToString() == "All products")
            ProductListView.ItemsSource = bl.Product.ProductListRequest();
        else
            ProductListView.ItemsSource = bl.Product.ProductListRequest(productForLists => productForLists!.Category == (BO.Categories)categorySelector.SelectedItem);
    }

    private void addProductButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow(bl).ShowDialog();

        ProductListView.ItemsSource = bl.Product.ProductListRequest();
    }

    private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (ProductListView.SelectedItem is BO.ProductForList p)
        {
            new ProductWindow(bl, p.Id).ShowDialog();

            ProductListView.ItemsSource = bl.Product.ProductListRequest();

        }
    }
}