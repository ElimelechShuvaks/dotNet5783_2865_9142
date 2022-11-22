namespace BO;


/// <summary>
/// data of Order item.
/// </summary>
public class OrderItem
{   
    /// <summary>
    /// id of order item
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Name of order item
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Price of order item
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// ProductId of order item
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Amount of order item
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// TotalPrice of order item
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        return $@"OrderItem OrderId: {OrderId}
Name :{Name}
Product OrderId: {ProductId}
Price: {Price}
Amount: {Amount}
TotalPrice{TotalPrice}:
";
    }
}
