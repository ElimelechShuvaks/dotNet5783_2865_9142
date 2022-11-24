using DalApi;
using System.Net.Mail;
using System.Text.RegularExpressions;
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


            if (orderItem is null) //Not exsist.
            {
                if (product.InStock > 0 && product.ProductId > 99999)
                {
                    orderItem = new BO.OrderItem();
                    orderItem.ProductId = idProduct;
                    orderItem.Name = product.Name;
                    orderItem.Amount = 1;
                    orderItem.TotalPrice += orderItem.Price;
                    cart.Items.Add(orderItem);
                }
                else
                {
                    throw new Exception();
                }
            }
            else //if exsist in cart.
            {
                if (product.InStock > 0 && product.ProductId > 99999)
                {
                    orderItem.TotalPrice += orderItem.Price;
                    orderItem.Amount += 1;
                }
                else
                {
                    throw new Exception();
                }
            }
            cart.TotalPrice += orderItem.Price;
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

    //This function returns true or false if the email address is valid.
    private bool isValidEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public void ConfirmationOrderToCart(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
    {
        try
        {
            foreach (BO.OrderItem orderItem in cart.Items)
            {
                DO.Product product = dal.Product.Get(orderItem.ProductId);

                if (product.InStock > 0 && orderItem.Amount <= product.InStock &&
                     cart.CustomerName == string.Empty && cart.CustomerAdress == string.Empty && isValidEmail(cart.CustomerEmail))
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

 // הוספת פונקציה לריקון הסל