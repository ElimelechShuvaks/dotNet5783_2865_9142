﻿using DalApi;
using DO;

namespace Dal;

internal class DalOrder : IOrder
{
    public int Add(Order newOrder)
    {
        newOrder.Id = DataSource.Num_runOrder;
        DataSource._orders.Add(newOrder);
        return newOrder.Id;
    }

    public void Delete(int idNum)
    {
        DataSource._orders.Remove(Get(orderFunc => orderFunc?.Id == idNum));
    }

    public void Update(Order order)
    {
        int index = existOrder(order.Id);

        if (index != -1)
        {
            DataSource._orders[index] = order;
            return;
        }
        OtherFunctions.exceptionNotFound("order", order.Id);
    }

    private int existOrder(int id)
    {
        int index = DataSource._orders.FindIndex(order => order?.Id == id);
        return index;
    }

    public Order Get(Func<Order?, bool>? func)
    {
        if (DataSource._orders.FirstOrDefault(func!) is Order order)
        {
            return order;
        }
        throw new Exception();
    }

    public IEnumerable<Order?> GetList(Func<Order?, bool>? func = null)
    {
        bool check = func is null;
        return check ? DataSource._orders.Select(order => order) : DataSource._orders.Where(func!);
    }
}

