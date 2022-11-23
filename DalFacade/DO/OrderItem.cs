namespace DO;

/// <summary>
/// data of Order item.
/// </summary>
public struct OrderItem
{
    public int ItemId { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return $@"
Order item Id: {ItemId}
Product Id: {ProductId}
Order Id: {OrderId}
Price: {Price}
Amount: {Amount}
";
    }
}
