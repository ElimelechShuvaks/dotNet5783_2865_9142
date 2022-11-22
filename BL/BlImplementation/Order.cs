﻿using Dal;
using DalApi;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private IDal dal = new DalList();

    /// <summary>
    /// takes an order id, search a Suitable DO order and try to return a BO order.
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order GetDetailsOrder(int idOrder)
    {
        try
        {
            if (idOrder <= 0) // not invaild id.
                throw new NotImplementedException();

            DO.Order order = dal.Order.Get(idOrder); // asking for a DO order with this order id.

            //if (!checkValidDateTime(order)) // chek the valid of dates in order.
            //    throw new NotImplementedException();

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
        catch (Exception)
        {

            throw;
        }
    }

    /// <summary>
    /// try to take a list of DO orders and return a list of OrderForList.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList> OrderForListRequest()
    {
        try
        {
            List<BO.OrderForList> ordersForList = dal.Order.GetList().Select(order =>
             new BO.OrderForList
             {
                 Id = order.Id,
                 CustomerName = order.CustomerName,
                 Status = GetStatus(order),
                 AmountOfItems = dal.OrderItem.GetListItem(order.Id).Count(),
                 TotalPrice = GetDetailsOrder(order.Id).TotalPrice,
             }).ToList();

            return ordersForList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// update the shipDate in DO order and also return a BO order.
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
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
        catch (Exception)
        {

            throw;
        }

        throw new NotImplementedException();
    }

    /// <summary>
    /// update the delivery Date in DO order and also return a BO order.
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order OrderDeliveryUpdate(int idOrder)
    {
        try
        {
            if (GetDetailsOrder(idOrder).Status == BO.OrderStatus.SentedOrder)
            {
                DO.Order order = dal.Order.Get(idOrder);
                order.DeliveryDate = DateTime.Now;

                dal.Order.Update(order);
                return GetDetailsOrder(idOrder);
            }
        }
        catch (Exception)
        {

            throw;
        }

        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.OrderTracking OrderTracking(int idOrder)
    {
        try
        {
            DO.Order order = dal.Order.Get(idOrder);
            BO.OrderTracking orderTracking = new BO.OrderTracking
            {
                OrderId = order.Id,
                Status = GetStatus(order),
                tuplesOfDateAndDescription = new List<Tuple<DateTime, string>>
                {
                    Tuple.Create(order.OrderDate, "The order has been created"),
                    Tuple.Create(order.ShipDate, "The order has been sent"),
                    Tuple.Create(order.DeliveryDate, "The order is already provided")
                }
            };

            return orderTracking;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// help function to colculate the order status of a givei DO order.
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.OrderStatus GetStatus(DO.Order order)
    {
        if (order.DeliveryDate != DateTime.MinValue)
            return BO.OrderStatus.ArrivedOrder;

        if (order.ShipDate != DateTime.MinValue)
            return BO.OrderStatus.SentedOrder;

        return BO.OrderStatus.ConfirmedOrder;
    }
}
