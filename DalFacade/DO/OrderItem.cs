namespace DO;

/// <summary>
/// data of Order item.
/// </summary>
public struct OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return $@"Item ID: {Id}
Product ID: {ProductId}
Order ID: {OrderId}
Price: {Price}
Amount: {Amount}
";
    }
}
