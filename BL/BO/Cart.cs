namespace BO;

/// <summary>
/// class of Cart.
/// </summary>
public class Cart
{

    /// <summary>
    /// CustomerName of Cart
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// CustomerEmail of Cart
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// CustomerAdress of Cart
    /// </summary>
    public string CustomerAdress { get; set; }

    /// <summary>
    /// Items of Cart
    /// </summary>
    public List<OrderItem> Items { get; set; }

    /// <summary>
    /// TotalPrice of order item
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        return $@"Customer Name: {CustomerName}
Customer Email: {CustomerEmail}
Customer Adress: {CustomerAdress}
TotalPrice:{TotalPrice}
";
    }
}
