
//using Windows.UI.Xaml.Media;

namespace BO;

public class ProductForList
{

    /// <summary>
    ///  OrderId of Product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///  Name of Product.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///  Price of Product.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    ///  Category of Product.
    /// </summary>
    public Categories? Category { get; set; }

    /// <summary>
    /// Image of product.
    /// </summary>
    //public string? Image { get; set; }

    //public Uri Uri { set; get; }

    //public ImageSource ImageSource { get; set; }

    public override string ToString() => $@"
Product id: {Id}
Name: {Name}
Category: {Category}
Price: {Price}
";
}
