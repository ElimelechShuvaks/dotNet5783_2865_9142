
using BlApi;
namespace BlImplementation;

/// <summary>
/// inplementation of IBl interface, that holds the 3 main entities Product, Order and Cart.
/// </summary>
sealed public class Bl : IBl
{
    public IProduct Product => new Product();

    public IOrder Order => new Order();

    public ICart Cart => new Cart();
}
