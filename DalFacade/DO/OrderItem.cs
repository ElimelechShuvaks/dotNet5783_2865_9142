
namespace DO;

/// <summary>
/// data of Order item.
/// </summary>
public struct OrderItem
{
    private int id;
    private int productId;
    private int orderId;
    private double price;
    private int amount;

    public int Id { get => id; set => id = value; }
    public int ProductId { get => productId; set => productId = value; }
    public int OrderId { get => orderId; set => orderId = value; }
    public double Price { get => price; set => price = value; }
    public int Amount { get => amount; set => amount = value; }

    public override string ToString()
    {
        return $@"Item ID: {id}
Product ID: {productId}
Order ID: {orderId}
Price: {price}
Amount: {amount}";
    }
}
