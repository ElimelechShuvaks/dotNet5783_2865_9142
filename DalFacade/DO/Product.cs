
namespace DO;

/// <summary>
/// Details of a product
/// </summary>
public struct Product
{
    private int iD;
    private string name;
    private double price;
    private Categories category;
    private string image;
    private int inStock;

    public int ID { get => iD; set => iD = value; }
    public string Name { get => name; set => name = value; }
    public double Price { get => price; set => price = value; }
    public Categories Category { get => category; set => category = value; }
    public string Image { get => image; set => image = value; }
    public int InStock { get => inStock; set => inStock = value; }

    public override string ToString() => $@"Product ID: {ID}
Name: {Name}
Category: {Category}
Price: {Price}
Amount in stock: {InStock}
";
}
