using DalApi;
using System.ComponentModel.DataAnnotations;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    private IDal dal = new Dal.DalList();

    public BO.Cart AddToCart(BO.Cart cart, int idProduct)
    {
        try
        {
            BO.OrderItem? orderItem = cart.Items!.FirstOrDefault(orderItems => orderItems.ProductId == idProduct);

            DO.Product product = dal.Product.Get(productFunc => productFunc?.ProductId == idProduct);

            if (orderItem is null) //Not exsist in cart.
            {
                if (product.InStock > 0) // the product exsit in stock.
                {
                    orderItem = new BO.OrderItem
                    {
                        ProductId = idProduct,
                        Name = product.Name,
                        Amount = 1,
                        Price = product.Price,
                        TotalPrice = product.Price,
                    };

                    cart.Items!.Add(orderItem);
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
        catch (DO.EntityNotExistException ex)
        {
            throw new BO.EntityNotExistException(ex.Message, ex);
        }

        return cart;
    }

    public BO.Cart ProductUpdateCart(BO.Cart cart, int idProduct, int newQuantity)
    {
        var items = cart.Items!;

        BO.OrderItem? orderItem = items.FirstOrDefault(orderItems => orderItems.ProductId == idProduct);

        try
        {
            if (orderItem is not null) // order item exsit in cart
            {
                int QuantitySummary = newQuantity - orderItem.Amount;   // QuantitySummary Saves the difference between the new and old quantity.

                if (QuantitySummary > 0 && newQuantity != 0)
                {
                    for (int i = 0; i < QuantitySummary; i++)
                        AddToCart(cart, idProduct);
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
                    items.Remove(orderItem);
                }
            }
            else // order item douse not exsit in cart
            {
                throw new BO.EntityNotExistException("There is no order with this product id");
            }
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }

        return cart;
    }

    /// <summary>
    /// This function returns true or false if the email address is valid.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    private bool isValidEmail(string email) => new EmailAddressAttribute().IsValid(email);

    public void ConfirmationOrderToCart(BO.Cart cart)
    {
        try
        {
            var items = cart.Items!;

            if (items.Count() == 0)
                throw new BO.EmptyCartException("can't confirm an empty cart.");

            if (cart.CustomerName != null && cart.CustomerAdress !=null && isValidEmail(cart.CustomerEmail!))
            {
                List<DO.Product> products = null!;

                foreach (BO.OrderItem orderItem in cart.Items!)
                {
                    DO.Product product = dal.Product.Get(productFunc => productFunc?.ProductId == orderItem.ProductId);

                    if (product.InStock < 0 || orderItem.Amount > product.InStock)
                        throw new BO.NotExsitInStockException("the product is out of stock");

                    (products ??= new List<DO.Product>()).Add(product);
                }

                DO.Order order = new DO.Order
                {
                    CustomerName = cart.CustomerName,
                    CustomerAdress = cart.CustomerAdress,
                    CustomerEmail = cart.CustomerEmail,
                    OrderDate = DateTime.Now,
                    ShipDate = null,
                    DeliveryDate = null,
                };

                int orderNumber = dal.Order.Add(order);

                var orderItemAndProducts = items.Zip(products!).ToList();

                orderItemAndProducts.ForEach(orderItemAndProduct =>
                {
                    var orderItem = orderItemAndProduct.First;
                    var product = orderItemAndProduct.Second;

                    dal.OrderItem.Add(new DO.OrderItem
                    {
                        OrderId = orderNumber,
                        ProductId = orderItem.ProductId,
                        Amount = orderItem.Amount,
                        Price = orderItem.Price,
                    });

                    product.InStock -= orderItem.Amount;

                    dal.Product.Update(product);
                });

                ResetCart(cart); // reset the cart.
            }
        }

        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.EntityNotExistException ex)
        {
            throw new BO.EntityNotExistException(ex.Message, ex);
        }
    }

    public void ResetCart(BO.Cart cart)
    {
        // reset the cart
        cart.Items!.Clear();
        cart.TotalPrice = 0;
    }
}