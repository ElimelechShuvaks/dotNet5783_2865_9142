using BlApi;
using BO;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace PL.Products;

/// <summary>
/// Interaction logic for Product.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBl localBl;

    Action action;

    /// <summary>
    /// ctor with 1 parameter for add product.
    /// </summary>
    private BO.Product product1 = new();
    public ProductWindow(IBl bl, Action senderAction)
    {
        Action1 = action;
        localBl = bl;
        action = senderAction;

        InitializeComponent();

        categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Categories));
        productWindowButton.Content = "Add";
    }

    /// <summary>
    /// ctor with int parameter for update an exist product.
    /// </summary>
    /// <param name="bl"></param>
    /// <param name="ProductId"></param>
    public ProductWindow(IBl bl, int ProductId, Action senderAction)
    {
        action = senderAction;
        localBl = bl;

        InitializeComponent();
        product1 = localBl.Product.ProductDetailsManger(ProductId);
        DataContext = product1;
        categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Categories));
        productWindowButton.Content = "Update";
        idTextBox.IsEnabled = false;
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
                action();
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
                localBl.Product.UpdateProduct(product1);
                action();
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
