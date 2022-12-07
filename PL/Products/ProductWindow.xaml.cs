using BlApi;
using BO;
using DO;
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

        BO.Product product1 = new BO.Product();
        public ProductWindow(IBl bl)
        {
            localBl = bl;

            InitializeComponent();

            categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Categories));

            productWindowButton.Content = "Add";
        }
        public ProductWindow(IBl bl, int ProductId)
        {
            localBl = bl;
            InitializeComponent();

            categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            productWindowButton.Content = "Update";
            product1 = localBl.Product.ProductDetailsManger(ProductId);

            idTextBox.Text = product1.Id.ToString();
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
            else
            {
                try
                {
                    product1.Category = OtherFunctions.CategoryParse(categoryComboBox);
                    product1.Name = nameTextBox.Text.ToString();
                    product1.Price = int.Parse(priceTextBox.Text);
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
    }
}
