namespace DO;

/// <summary>
/// Details of a product
/// </summary>
public struct Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Categories Category { get; set; }
    public string Image { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"Product ProductId: {ProductId}
Name: {Name}
Category: {Category}
Price: {Price}
Amount in stock: {InStock}
";
}
