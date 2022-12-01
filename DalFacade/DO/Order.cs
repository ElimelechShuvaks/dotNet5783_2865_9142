namespace DO;

/// <summary>
/// struct to represent data of an order
/// </summary>
public struct Order
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public override string ToString()
    {
        return $@"Order Number: {Id}
Customer Name: {CustomerName}
Customer Email: {CustomerEmail}
Customer Adress: {CustomerAdress}
Order Date: {OrderDate}
Ship Date: {ShipDate?.ToShortDateString()}
Delivery Date: {DeliveryDate}
";
    }
}
