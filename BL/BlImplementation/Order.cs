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
                var (items, sum) = getOrders(order.Id);

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

                    Items = items.Select(_orderItem =>
                    {
                        DO.OrderItem orderItem = _orderItem!.Value;

                        return new BO.OrderItem
                        {
                            OrderId = orderItem.OrderId,
                            ProductId = orderItem.ProductId,
                            Name = dal.Product.Get(orderItem.ProductId).Name,
                            Price = orderItem.Price,
                            Amount = orderItem.Amount,
                            TotalPrice = orderItem.Price * orderItem.Amount
                        };

                    }).ToList(),

                    TotalPrice = sum,
                };

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

    private (IEnumerable<DO.OrderItem?>, double) getOrders(int orderId)
    {
        var orderItems = dal.OrderItem.GetList(orderItem => orderItem!.Value.OrderId == orderId);
        return (orderItems, orderItems.Sum(orderItem => orderItem!.Value.Amount * orderItem.Value.Price));
    }

    public IEnumerable<BO.OrderForList> OrderForListRequest()
    {
        try
        {
            return dal.Order.GetList().Select(order =>
            {
                DO.Order _order = order!.Value;
                var (items, sum) = getOrders(_order.Id);

                return new BO.OrderForList
                {
                    OrderId = _order.Id,
                    CustomerName = _order.CustomerName,
                    Status = GetStatus(_order),
                    AmountOfItems = items.Count(),
                    TotalPrice = sum,
                };

            }).ToList();
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

            if (boOrder.Status is BO.OrderStatus.Confirmed)
            {
                DO.Order order = dal.Order.Get(idOrder);
                order.ShipDate = DateTime.Now;

                dal.Order.Update(order);

                boOrder.ShipDate = DateTime.Now;
                return boOrder;
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

                boOrder.DeliveryDate = DateTime.Now;

                return boOrder;
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
                tuplesOfDateAndDescription = new List<(DateTime?, string?)> ()
            };

            switch (orderTracking.Status)
            {
                case BO.OrderStatus.Confirmed:

                    orderTracking.tuplesOfDateAndDescription.Add((order.OrderDate, "The order has been created"));
                    break;

                case BO.OrderStatus.Shipied:

                    orderTracking.tuplesOfDateAndDescription.Add((order.OrderDate, "The order has been created"));
                    orderTracking.tuplesOfDateAndDescription.Add((order.ShipDate, "The order has been sent"));
                    break;

                case BO.OrderStatus.Deliveried:
                    orderTracking.tuplesOfDateAndDescription.Add((order.OrderDate, "The order has been created"));
                    orderTracking.tuplesOfDateAndDescription.Add((order.ShipDate, "The order has been sent"));
                    orderTracking.tuplesOfDateAndDescription.Add((order.DeliveryDate, "The order is deliveried"));
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
        if (order.DeliveryDate != null)
            return BO.OrderStatus.Deliveried;

        if (order.ShipDate != null)
            return BO.OrderStatus.Shipied;

        return BO.OrderStatus.Confirmed;
    }

    public void OrderUpdate(int orderId, int productId, int newAmount)
    {
        try
        {
            if (GetStatus(dal.Order.Get(orderId)) != BO.OrderStatus.Confirmed) // check if the order is'n sent.
                throw new BO.StatusErrorException("cnn't updating the order becouse it's alredy sent.");

            if (dal.Order.GetList().ToList().Any(order => order?.Id == orderId)) // check if it exsit an order with this id.
            {
                DO.OrderItem orderItem = dal.OrderItem.Get(item => item?.OrderId == orderId &&
                item?.ProductId == productId);

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