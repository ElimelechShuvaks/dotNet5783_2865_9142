
namespace DO;

/// <summary>
/// 
/// </summary>
public struct Product
{
    public override string ToString() => $" Product ID= {ID} Name= {Name}, Category - {Category} Company_{Company} Price: {Price}Amount in stock: {InStock}";



    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Categories Category { get; set; }
    public string Company { get; set; }
    public string Image { get; set; }
    public int InStock { get; set; }
}
