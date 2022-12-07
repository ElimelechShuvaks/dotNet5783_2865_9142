using BlApi;
using BO;
using System;
using System.Collections.ObjectModel;
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

        public ProductWindow(IBl bl)
        {
            localBl = bl;

            InitializeComponent();

            categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Categories));

            productWindowButton.Content = "Add";
        }
        public ProductWindow(IBl bl, ActionCase actionCase, int ProductId)
        {
            localBl = bl;
            InitializeComponent();

            categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            productWindowButton.Content = "Update";
            BO.Product product = localBl.Product.ProductDetailsManger(ProductId);

            idTextBox.Text = product.Id.ToString();
            categoryComboBox.Text = product.Category.ToString();
            NameTextBox.Text = product.Name!.ToString();
            PriceTextBox.Text = product.Price.ToString();
            AmountTextBox.Text = product.InStock.ToString();

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
