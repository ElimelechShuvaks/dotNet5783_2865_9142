
namespace DO;

/// <summary>
/// struct to represent data of an order
/// </summary>
public struct Order
{
    private int id;
    private string customerName;
    private string customerEmail;
    private string customerAdress;
    private DateTime orderDate;
    private DateTime shipDate;
    private DateTime deliveryDate;

    public int Id { get => id; set => id = value; }
    public string CustomerName { get => customerName; set => customerName = value; }
    public string CustomerEmail { get => customerEmail; set => customerEmail = value; }
    public string CustomerAdress { get => customerAdress; set => customerAdress = value; }
    public DateTime OrderDate { get => orderDate; set => orderDate = value; }
    public DateTime ShipDate { get => shipDate; set => shipDate = value; }
    public DateTime DeliveryDate { get => deliveryDate; set => deliveryDate = value; }

    public override string ToString()
    {
        return $@"Order Number: {id}
Customer Name: {customerName}
Customer Email: {customerEmail}
Customer Adress: {customerAdress}
Order Date: {orderDate}
Ship Date: {shipDate.ToShortDateString()}
Delivery Date: {deliveryDate}
";
    }
}
