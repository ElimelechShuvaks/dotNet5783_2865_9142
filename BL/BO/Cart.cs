using OtherFunction;

namespace BO;

/// <summary>
/// class of Cart.
/// </summary>
public class Cart
{
    /*We have removed the name, address, and email fields in the shopping cart, 
    because there are duplicates here, and therefore we do accept data about the person as parameters*/

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
        return $@"Items: {Items.GetToStrings()}
TotalPrice:{TotalPrice}
";
    }
}
