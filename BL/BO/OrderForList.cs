namespace BO;

/// <summary>
/// class for OrderForList.
/// </summary>
public class OrderForList
{

    /// <summary>
    /// id of Order For List
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// CustomerName of Order For List
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// status of Order For List
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// AmountOfItems of Order For List
    /// </summary>
    public int AmountOfItems { get; set; }
    /// <summary>
    /// TotalPrice of Order For List
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        return $@"Order id: {OrderId}
Customer name :{CustomerName}
Order status: {Status}
Amount of items: {AmountOfItems}
Total price{TotalPrice}:
";
    }
}
