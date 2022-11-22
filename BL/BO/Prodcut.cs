namespace BO;

/// <summary>
/// Main logical entity of a product (Product)
/// - for screens of product details (for management) and operations on a product.
/// </summary>
public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public Categories Category { get; set; }

    public string Image { get; set; }

    public int InStock { get; set; }

    public override string ToString() => $@"Product Id: {Id}
    public override string ToString() => $@"Product ID: {Id}
Name: {Name}
Category: {Category}
Price: {Price}
Amount in stock: {InStock}
";
}
