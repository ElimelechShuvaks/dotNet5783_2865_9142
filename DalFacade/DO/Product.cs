
namespace DO;

/// <summary>
/// Details of a product
/// </summary>
public struct Product
{
    public override string ToString() => $@"Product ID: {ID}
Name: {Name}
Category: {Category}
Price: {Price}
";

    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Categories Category { get; set; }
   // public string Company { get; set; }//הורדתי את זה לבנתתים זה קצת מסבך
  //  public string Image { get; set; }
    public int InStock { get; set; }
}
