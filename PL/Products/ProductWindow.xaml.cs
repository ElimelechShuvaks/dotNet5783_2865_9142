using BlApi;
using BO;
using DO;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace PL.Products;

/// <summary>
/// Interaction logic for Product.xaml
/// </summary>
public partial class ProductWindow : Window
{
    IBl localBl;

    /// <summary>
    /// ctor with 1 parameter for add product.
    /// </summary>
    BO.Product product1 = new BO.Product();
    public ProductWindow(IBl bl)
    {
        localBl = bl;

        InitializeComponent();

        categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Categories));

        productWindowButton.Content = "Add";
    }

    /// <summary>
    /// ctor with int parameter for update an exist product.
    /// </summary>
    /// <param name="bl"></param>
    /// <param name="ProductId"></param>
    public ProductWindow(IBl bl, int ProductId)
    {
        localBl = bl;
        InitializeComponent();

        categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Categories));
        productWindowButton.Content = "Update";
        product1 = localBl.Product.ProductDetailsManger(ProductId);

        idTextBox.Text = product1.Id.ToString();
        idTextBox.IsEnabled = false;
        categoryComboBox.Text = product1.Category.ToString();
        nameTextBox.Text = product1.Name!.ToString();
        priceTextBox.Text = product1.Price.ToString();
        inStockTextBox.Text = product1.InStock.ToString();
    }

    private void productWindowButton_Click(object sender, RoutedEventArgs e)
    {
        if (productWindowButton.Content.ToString() == "Add")
        {
            BO.Product product = new BO.Product()
            {
                Category = OtherFunctions.CategoryParse(categoryComboBox),
                Name = nameTextBox.Text.ToString(),
                Id = int.Parse(idTextBox.Text),
                Price = double.Parse(priceTextBox.Text),
                InStock = int.Parse(inStockTextBox.Text)
            };

            try
            {
                localBl.Product.AddProduct(product);
                Close();
            }
            catch (BlExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else // button content is "update".
        {
            try
            {
                product1.Category = OtherFunctions.CategoryParse(categoryComboBox);
                product1.Name = nameTextBox.Text.ToString();
                product1.Price = double.Parse(priceTextBox.Text);
                product1.InStock = int.Parse(inStockTextBox.Text);

                localBl.Product.UpdateProduct(product1);
                Close();
            }
            catch (BlExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    /// <summary>
    /// private helpe function to prevent the user to type non number digits in text box.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IntegerValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]");
        e.Handled = regex.IsMatch(e.Text);
    }

    /// <summary>
    /// private helpe function to prevent the user to type non numbers or decimal numbers in text box.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DoubleValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9.]");
        e.Handled = regex.IsMatch(e.Text);
    }
}
