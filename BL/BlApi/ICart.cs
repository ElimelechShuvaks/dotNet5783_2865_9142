using BO;

namespace BlApi;

/// <summary>
/// Actions on shopping cart (all for buyer screens only).
/// </summary>
public interface ICart
{
    /// <summary>
    /// Adding a product to the shopping cart (for catalog screen, product details screen).
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="idProduct"></param>
    /// <returns></returns>
    public Cart AddCart(Cart cart, int idProduct);

    /// <summary>
    /// Updating the quantity of a product in the shopping cart (for the shopping cart screen)
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="idProduct"></param>
    /// <param name="newQuantity"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Cart ProductUpdateCart(Cart cart, int idProduct, int newQuantity);

    /// <summary>
    /// Basket confirmation for order \ placing an order (for shopping basket screen or order completion screen).
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="customerName"></param>
    /// <param name="customerEmail"></param>
    /// <param name="customerAdress"></param>
    /// <exception cref="NotImplementedException"></exception>
    /// 
    public void ConfirmationOrderToCart(Cart cart, string customerName, string customerEmail, string customerAdress);
}

