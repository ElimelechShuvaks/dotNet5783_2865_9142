using BO;
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
            else // exsist.
            {
                if (product.InStock > 0)
                {
                    orderItem.Price = product.Price;
                    orderItem.TotalPrice += orderItem.Price;
                    orderItem.Amount += 1;
                }
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

    public BO.Cart ProductUpdateCart(BO.Cart cart, int idProduct, int newQuantity)
    {
        BO.OrderItem orderItem = cart.Items.FirstOrDefault(cartItem => cartItem.OrderId == idProduct);
        try
        {
            if (orderItem is not null)
            {
                int QuantitySummary = newQuantity - orderItem.Amount;   // QuantitySummary Saves the difference between the new and old quantity.

                if (QuantitySummary > 0)
                {
                    for (int i = 0; i < QuantitySummary; i++)
                        AddCart(cart, idProduct);
                }
                if (QuantitySummary < 0)
                {
                    orderItem.Amount = newQuantity;
                    orderItem.TotalPrice = newQuantity * orderItem.Price;
                    cart.TotalPrice -= -1 * (QuantitySummary * orderItem.Price);
                }
                if (newQuantity == 0)
                {
                    cart.TotalPrice -= orderItem.Amount * orderItem.Price;
                    cart.Items.Remove(orderItem);
                }
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {

            throw new Exception();
        }
        return cart;
    }

    //**I'm not done with this function**.

    public void ConfirmationOrderToCart(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
    {
        foreach (OrderItem orderItem in cart.Items)
        {
            DO.Product product = dal.Product.Get(orderItem.ProductId);

            if (product.InStock > 0 && orderItem.Amount <= product.InStock &&
                 cart.CustomerName is not null && cart.CustomerAdress is not null && cart.CustomerEmail is not null)
            {
                //Id?
                DO.Order order = new DO.Order();
                order.CustomerName = customerName;
                order.CustomerAdress = customerAdress;
                order.CustomerEmail = customerEmail;
                order.OrderDate = DateTime.Now;
                order.ShipDate = DateTime.MinValue;
                order.DeliveryDate = DateTime.MinValue;
                int orderNumber = dal.Order.Add(order);

                DO.OrderItem orderItem1 = new DO.OrderItem();


            }
        }
        throw new NotImplementedException();
    }


}

