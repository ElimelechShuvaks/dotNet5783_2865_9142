﻿using Dal;
using DalApi;
using System.Security.AccessControl;

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
            catch (DO.IdNotExistException)
            {
                throw new BO.IdNotExistException($"Order with id: {idOrder} doesn't exsist in data source");
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
            DO.Order order = dal.Order.Get(idOrder);
            if (order.ShipDate == DateTime.MinValue)
            {
                order.ShipDate = DateTime.Now; // change the shipDate to DateTime.Now
                dal.Order.Update(order); // for update the order in data sourse

                BO.Order retOrder = GetDetailsOrder(idOrder);
                return retOrder;
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
                throw new BO.StatusErrorException($"can't deliveried becouse the status is {boOrder.Status}");
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
            throw new BO.IdNotExistException(ex.Message);
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

    public BO.Order OrderUpdate(int orderId, int productId, int newAmount)
    {
        if (dal.Order.GetList().ToList().Any(order => order.Id == orderId))
        {
            DO.OrderItem orderItem = dal.OrderItem.GetList().ToList().FirstOrDefault(item => item.OrderId == orderId && item.ProductId == productId);

            if (orderItem.Equals(default(DO.OrderItem))) // there is'n a order item with these ids, than add a new order item with this produc
            {

            }
            else // the product alredy exist in the order, than 
            {

            }
        }
        else
        {
            throw new BO.IdNotExistException($"There is no order with such id: {orderId}");
        }
    }
}