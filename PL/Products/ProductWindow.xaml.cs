using BlApi;
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
            //if (productWindowButton.Content == "Add")
            //    localBl.Product.AddProduct(new BO.Product
            //    {
            //        //Id = idTextBox.Text
            //        Category = (BO.Categories)int.Parse(categoryComboBox.Text),
            //        Name = 

            //    });
        }
    }
}
