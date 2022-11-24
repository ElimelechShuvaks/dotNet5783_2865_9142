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
    public string Name { get; set; }

    /// <summary>
    ///  Price of Product.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    ///  Category of Product.
    /// </summary>
    public Categories Category { get; set; }

    public override string ToString() => $@"Product OrderId: {Id}
Name: {Name}
Category: {Category}
Price: {Price}
";
}
