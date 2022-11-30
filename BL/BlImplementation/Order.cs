using Dal;
using DalApi;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private IDal dal = new DalList();

    public BO.Order GetDetailsOrder(int idOrder)
    {
        if (idOrder > 0) // vaild id.
        {
            try
            {
                DO.Order order = dal.Order.Get(idOrder); // asking for a DO order with this order id.

                BO.Order retOrdrt = new BO.Order
                {
                    Id = order.Id,
                    CustomerName = order.CustomerName,
                    CustomerEmail = order.CustomerEmail,
                    CustomerAdress = order.CustomerAdress,
                    OrderDate = order.OrderDate,
                    Status = GetStatus(order),
                    ShipDate = order.ShipDate,
                    DeliveryDate = order.DeliveryDate,

                    Items = dal.OrderItem.GetListItem(idOrder).Select(orderItem =>
                    new BO.OrderItem
                    {
                        OrderId = orderItem.OrderId,
                        ProductId = orderItem.ProductId,
                        Name = dal.Product.Get(orderItem.ProductId).Name,
                        Price = orderItem.Price,
                        Amount = orderItem.Amount,
                        TotalPrice = orderItem.Price * orderItem.Amount
                    }).ToList()
                };

                retOrdrt.TotalPrice = retOrdrt.Items.Sum(orderItem => orderItem.TotalPrice);

                return retOrdrt;
            }
            catch (DO.IdNotExistException ex)
            {
                throw new BO.IdNotExistException($"Order with id: {idOrder} doesn't exsist in data source", ex);
            }
        }
        else // unvalide id
        {
            throw new BO.IdNotValidException("not valid id for order");
        }
    }

    public IEnumerable<BO.OrderForList> OrderForListRequest()
    {
        try
        {
            List<BO.OrderForList> ordersForList = dal.Order.GetList().Select(order =>
             new BO.OrderForList
             {
                 OrderId = order.Id,
                 CustomerName = order.CustomerName,
                 Status = GetStatus(order),
                 AmountOfItems = dal.OrderItem.GetListItem(order.Id).Count(),
                 TotalPrice = GetDetailsOrder(order.Id).TotalPrice,
             }).ToList();

            return ordersForList;
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
    }

    public BO.Order OrderShippingUpdate(int idOrder)
    {
        try
        {
            BO.Order boOrder = GetDetailsOrder(idOrder);

            if (boOrder.Status == BO.OrderStatus.Confirmed)
            {
                DO.Order order = dal.Order.Get(idOrder);
                order.ShipDate = DateTime.Now;

                dal.Order.Update(order);
                return GetDetailsOrder(idOrder);
            }
            else
            {
                throw new BO.StatusErrorException($"can't change the status to shiped becouse the status is {boOrder.Status}");
            }
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message, ex);
        }
    }

    public BO.Order OrderDeliveryUpdate(int idOrder)
    {
        try
        {
            BO.Order boOrder = GetDetailsOrder(idOrder);

            if (boOrder.Status == BO.OrderStatus.Shipied)
            {
                DO.Order order = dal.Order.Get(idOrder);
                order.DeliveryDate = DateTime.Now;

                dal.Order.Update(order);
                return GetDetailsOrder(idOrder);
            }
            else
            {
                throw new BO.StatusErrorException($"can't change the status to deliveried becouse the status is {boOrder.Status}");
            }
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message, ex);
        }
    }

    public BO.OrderTracking OrderTracking(int idOrder)
    {
        try
        {
            DO.Order order = dal.Order.Get(idOrder);
            BO.OrderTracking orderTracking = new BO.OrderTracking
            {
                OrderId = order.Id,
                Status = GetStatus(order),
                tuplesOfDateAndDescription = new List<Tuple<DateTime, string>>()
            };

            switch (orderTracking.Status)
            {
                case BO.OrderStatus.Confirmed:

                    orderTracking.tuplesOfDateAndDescription.Add(Tuple.Create(order.OrderDate, "The order has been created"));
                    break;

                case BO.OrderStatus.Shipied:

                    orderTracking.tuplesOfDateAndDescription.Add(Tuple.Create(order.OrderDate, "The order has been created"));
                    orderTracking.tuplesOfDateAndDescription.Add(Tuple.Create(order.ShipDate, "The order has been sent"));
                    break;

                case BO.OrderStatus.Deliveried:
                    orderTracking.tuplesOfDateAndDescription.Add(Tuple.Create(order.OrderDate, "The order has been created"));
                    orderTracking.tuplesOfDateAndDescription.Add(Tuple.Create(order.ShipDate, "The order has been sent"));
                    orderTracking.tuplesOfDateAndDescription.Add(Tuple.Create(order.DeliveryDate, "The order is deliveried"));
                    break;

                default:
                    break;
            }

            return orderTracking;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message, ex);
        }
    }

    /// <summary>
    /// help function to colculate the order status of a given DO order.
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.OrderStatus GetStatus(DO.Order order)
    {
        if (order.DeliveryDate != DateTime.MinValue)
            return BO.OrderStatus.Deliveried;

        if (order.ShipDate != DateTime.MinValue)
            return BO.OrderStatus.Shipied;

        return BO.OrderStatus.Confirmed;
    }

    public void OrderUpdate(int orderId, int productId, int newAmount)
    {
        try
        {
            if (GetStatus(dal.Order.Get(orderId)) != BO.OrderStatus.Confirmed) // check if the order is'n sent.
                throw new BO.StatusErrorException("cnn't updating the order becouse it's alredy sent.");

            if (dal.Order.GetList().ToList().Any(order => order.Id == orderId)) // check if it exsit an order with this id.
            {
                DO.OrderItem orderItem = dal.OrderItem.GetList().ToList().FirstOrDefault(item => item.OrderId == orderId && item.ProductId == productId);

                DO.Product product = dal.Product.Get(productId);

                if (orderItem.Equals(default(DO.OrderItem))) // there is'n an order item with these ids, than add a new order item with this produc
                {
                    if (product.InStock < newAmount) // check if there is enough in stock
                        throw new BO.NotExsitInStockException("the product is out of stock");

                    orderItem.OrderId = orderId;
                    orderItem.ProductId = product.ProductId;
                    orderItem.Amount = newAmount;
                    orderItem.Price = product.Price * newAmount;

                    dal.OrderItem.Add(orderItem);
                }
                else // the product alredy exist in the order, than 
                {
                    if (product.InStock < newAmount) // check if there is enough in stock
                        throw new BO.NotExsitInStockException("the product is out of stock");

                    orderItem.Amount = newAmount;

                    dal.OrderItem.Update(orderItem);
                }
            }
            else
            {
                throw new BO.IdNotExistException($"There is no order with such id: {orderId}");
            }
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message, ex);
        }
    }
}