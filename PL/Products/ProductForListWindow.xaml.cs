using BlApi;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL.Products;

/// <summary>
/// Interaction logic for ProductForListWindow.xaml
/// </summary>
public partial class ProductForListWindow : Window
{
    public static readonly DependencyProperty ProductForListProperty = DependencyProperty.Register(nameof(ProductForList), typeof(IEnumerable<BO.ProductForList>), typeof(ProductForListWindow));

    public IEnumerable<BO.ProductForList> ProductForList { get => (IEnumerable<BO.ProductForList>)GetValue(ProductForListProperty); set => SetValue(ProductForListProperty, value); }

    IBl bl = Factory.get();

    public ProductForListWindow()
    {
        ProductForList = bl.Product.ProductListRequest()!;
        InitializeComponent();

        for (int i = 0; i < 5; i++) // include the 5 categories into the comboBox.
        {
            categorySelector.Items.Add((BO.Categories)i);
        }
        categorySelector.Items.Add("All products"); // add a basic condition.

    }

    private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (categorySelector.SelectedItem.ToString() == "All products")
            ProductForList = bl.Product.ProductListRequest()!;
        else
            ProductForList = bl.Product.ProductListRequest(productForLists => productForLists!.Category == (BO.Categories)categorySelector.SelectedItem)!;
    }

    /// <summary>
    /// Adding a product by the managerץ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void addProductButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow(bl,this).ShowDialog();
    }

    private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (ProductListView.SelectedItem is BO.ProductForList p)
        
            new ProductWindow(bl, p.Id,this).ShowDialog();
    }
}