using DalApi;
namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    private IDal dal = new Dal.DalList();

    public BO.Cart AddCart(BO.Cart cart, int idProduct)
    {
        try
        {
            BO.OrderItem orderItem = cart.Items.FirstOrDefault(cartItem => cartItem.OrderId == idProduct);
            DO.Product product = dal.Product.Get(idProduct);


            if (orderItem is null)
            {
                if (product.InStock > 0)
                {
                    orderItem.ProductId = idProduct;
                    orderItem.Price = product.Price;
                    orderItem.Name = product.Name;
                    orderItem.Amount += 1;
                    orderItem.TotalPrice += orderItem.Price;
                    cart.Items.Add(orderItem);

                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                if (product.InStock > 0)
                {
                    orderItem.Amount += 1;                }
                else
                {
                    throw new Exception();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        return cart;
    }

    public BO.Cart ProductUpdateCart(BO.Cart cart, int idCart, int newQuantity)
    {
        throw new NotImplementedException();
    }

    public void ConfirmationOrderToCart(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
    {



        throw new NotImplementedException();
    }


}

