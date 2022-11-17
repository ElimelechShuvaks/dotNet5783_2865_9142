using BO;

namespace BlApi;

/// <summary>
/// Interface for cart.
/// </summary>
public interface ICart
{
    /// <summary>
    /// Add cart and check.
    /// </summary>
    /// <param name="cart"></param>
    /// <returns></returns>
    public Cart AddCart(Cart cart, int idCart);

    /// <summary>
    /// Product update cart.
    /// </summary>
    /// <param name="cart"></param>
    /// <returns></returns>
    public Cart ProductUpdateCart(Cart cart, int idCart, int newQuantity);

    /// <summary>
    /// Basket confirmation for order / placing an order.
    /// </summary>
    /// <param name="CustomerName"></param>
    /// <param name="CustomerEmail"></param>
    /// <param name="CustomerAdress"></param>
    public void ConfirmationOrderToCart(Cart cart, string customerName, string customerEmail, string customerAdress);
}