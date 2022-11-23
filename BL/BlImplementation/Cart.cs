using BO;
using DalApi;
namespace BlImplementation;

/// <summary>
/// Actions on shopping cart (all for buyer screens only).
/// </summary>
internal class Cart : BlApi.ICart
{
    private IDal dal = new Dal.DalList();

    /// <summary>
    /// Adding a product to the shopping cart (for catalog screen, product details screen).
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="idProduct"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Updating the quantity of a product in the shopping cart (for the shopping cart screen)
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="idProduct"></param>
    /// <param name="newQuantity"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// Basket confirmation for order \ placing an order (for shopping basket screen or order completion screen).
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="customerName"></param>
    /// <param name="customerEmail"></param>
    /// <param name="customerAdress"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void ConfirmationOrderToCart(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
    {
        try
        {
            foreach (OrderItem orderItem in cart.Items)
            {
                DO.Product product = dal.Product.Get(orderItem.ProductId);

                if (product.InStock > 0 && orderItem.Amount <= product.InStock &&
                     cart.CustomerName is not null && cart.CustomerAdress is not null && cart.CustomerEmail is not null)
                {
                    DO.Order order = new DO.Order();

                    order.CustomerName = customerName;
                    order.CustomerAdress = customerAdress;
                    order.CustomerEmail = customerEmail;
                    order.OrderDate = DateTime.Now;
                    order.ShipDate = DateTime.MinValue;
                    order.DeliveryDate = DateTime.MinValue;
                    int orderNumber = dal.Order.Add(order);

                    DO.OrderItem ToAddOrderItem = new DO.OrderItem();

                    ToAddOrderItem.ProductId = orderItem.ProductId;
                    ToAddOrderItem.OrderId = orderNumber;
                    ToAddOrderItem.Amount = orderItem.Amount;
                    ToAddOrderItem.Price = orderItem.Price;
                    dal.OrderItem.Add(ToAddOrderItem);

                    product.InStock = product.InStock - orderItem.Amount;
                    dal.Product.Update(product);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}

