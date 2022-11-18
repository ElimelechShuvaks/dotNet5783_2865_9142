namespace BO;

/// <summary>
/// claas for Product Item.
/// </summary>
public class ProductItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Enums.Categories Category { get; set; }
    // public string Image { get; set; }

    /// <summary>
    /// Amount of Product Item
    /// </summary>
    public int Amount { get; set; }
    public int InStock { get; set; }


    public override string ToString() => $@"Product ID: {ID}
Name: {Name}
Category: {Category}
Price: {Price}
Amount:{Amount}
Amount in stock: {InStock}
";
}
