using BlApi;
using BO;
using System;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Windows;


namespace PL.Products
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        IBl localBl;

        public ProductWindow(IBl bl, ActionCase actionCase)
        {
            localBl = bl;

            InitializeComponent();

            categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Categories));

            if (actionCase == ActionCase.Add)
                productWindowButton.Content = "Add";
            else
                productWindowButton.Content = "Update";
        }

        private void productWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (productWindowButton.Content.ToString() == "Add")
            {
                Product product = new Product()
                {
                    Category = OtherFunctions.CategoryParse(categoryComboBox),
                    Name = nameTextBox.Text.ToString(),
                    Id = int.Parse(idTextBox.Text),
                    Price = int.Parse(priceTextBox.Text),
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
        }
    }
}
