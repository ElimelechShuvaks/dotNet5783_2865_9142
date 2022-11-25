using DalApi;
using System.Net.Mail;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    private IDal dal = new Dal.DalList();

    public BO.Cart AddCart(BO.Cart cart, int idProduct)
    {
        try
        {
            BO.OrderItem orderItem = cart.Items.FirstOrDefault(orderItems => orderItems.ProductId == idProduct);
            DO.Product product = dal.Product.Get(idProduct);

            if (orderItem is null) //Not exsist in cart.
            {
                if (product.InStock > 0) // the product exsit in stock.
                {
                    orderItem = new BO.OrderItem();
                    orderItem.ProductId = idProduct;
                    orderItem.Name = product.Name;
                    orderItem.Amount = 1;
                    orderItem.TotalPrice = orderItem.Price;
                    cart.Items.Add(orderItem);
                }
                else
                {
                    throw new BO.NotExsitInStockException("the product is out of stock");
                }
            }
            else //if exsist in cart.
            {
                if (product.InStock > 0)
                {
                    orderItem.TotalPrice += orderItem.Price;
                    orderItem.Amount += 1;
                }
                else
                {
                    throw new BO.NotExsitInStockException("the product is out of stock");
                }
            }
            cart.TotalPrice += orderItem.Price;
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message);
        }

        return cart;
    }

    public BO.Cart ProductUpdateCart(BO.Cart cart, int idProduct, int newQuantity)
    {
        BO.OrderItem orderItem = cart.Items.FirstOrDefault(orderItems => orderItems.ProductId == idProduct);
        try
        {
            if (orderItem is not null) // order item exsit in cart
            {
                int QuantitySummary = newQuantity - orderItem.Amount;   // QuantitySummary Saves the difference between the new and old quantity.

                if (QuantitySummary > 0 && newQuantity != 0)
                {
                    for (int i = 0; i < QuantitySummary; i++)
                        AddCart(cart, idProduct);
                }
                if (QuantitySummary < 0 && newQuantity != 0)
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
            else // order item douse not exsit in cart
            {
                throw new BO.IdNotExistException("There is no order with this product id");
            }
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
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

    /*We have removed the name, address, and email fields in the shopping cart, 
    because there are duplicates here, and therefore we do accept data about the person as parameters*/
    public void ConfirmationOrderToCart(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
    {
        try
        {
            foreach (BO.OrderItem orderItem in cart.Items)
            {
                DO.Product product = dal.Product.Get(orderItem.ProductId);

                if (product.InStock < 0 || orderItem.Amount > product.InStock)
                    throw new BO.NotExsitInStockException("the product is out of stock");

                if (customerName != string.Empty && customerAdress != string.Empty && isValidEmail(customerEmail))
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
                else // the name or mail or address is invalid.
                {
                    throw new BO.InvalidPersonDetails("the name or mail or address is invalid.");
                }
            }
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message);
        }
    }

    public void RemoveCart(BO.Cart cart)
    {
        try
        {
            foreach (BO.OrderItem orderItem in cart.Items)
            {
                ProductUpdateCart(cart, orderItem.ProductId, 0);
            }
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
    }
}

