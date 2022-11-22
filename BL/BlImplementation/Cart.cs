using DalApi;
namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    public BO.Cart AddCart(BO.Cart cart, int idCart)
    {
        BO.OrderItem orderItem = cart.Items.FirstOrDefault(cartItem => cartItem.OrderId == idCart);
        //if (orderItem == null)
        //{

        //}
        return cart;
    }

    public void ConfirmationOrderToCart(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
    {
        throw new NotImplementedException();
    }

    public BO.Cart ProductUpdateCart(BO.Cart cart, int idCart, int newQuantity)
    {
        throw new NotImplementedException();
    }
}

