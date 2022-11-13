using DO;
using DalApi;
using System;

namespace Dal;

public class DalOrderitem : IOrderItem
{

    //public OrderItem getBy_2Id(int pId, int oId)
    //{
    //    foreach (OrderItem orderitem in DataSource._orderItems)
    //    {
    //        if (orderitem.ProductId == pId && orderitem.OrderId == oId)
    //        {
    //            return orderitem;
    //        }
    //    }
    //    throw new Exception("not found the orderitem");
    //}

    //public OrderItem[] getItemArray(int orderId)
    //{
    //    int num;
    //    num = DataSource._orderItems.Count(o => o.OrderId == orderId);
    //    OrderItem[] items = new OrderItem[num];
    //    int i = 0;
    //    foreach (OrderItem orderitem in DataSource._orderItems)
    //    {
    //        if (orderitem.OrderId == orderId)
    //        {
    //            items[i++] = orderitem;
    //        }
    //    }
    //    return items;
    //}


 
    public int Add(OrderItem newOrderItem)
    {
        newOrderItem.Id = DataSource.Config.Num_runOrderitem;
        
        DataSource._orderItems.Add(newOrderItem);
 
        return newOrderItem.Id;
    }

    public void Delete(int idNum)
    {
        OrderItem? orderItem = Get(idNum);

        if (orderItem != null)
        {
            DataSource._orderItems.Remove(orderItem.Value);
            return;
        }
        OtherFunctions.exceptionNotFound("order item", idNum);
    }

    public OrderItem Get(int idNum)
    {
        int index = existOrderItem(idNum);

        if (index == -1)
        {
            OtherFunctions.exceptionNotFound("order item", idNum);
        }
        return DataSource._orderItems[index];
    }

    public OrderItem GetBuy_2Id(int pId, int oId)
    {
        int index = DataSource._orderItems.FindIndex(orderItem => orderItem.ProductId == pId && orderItem.OrderId == oId);

        if (index == -1)
        {
            OtherFunctions.exceptionNotFound("order item", pId, oId);
        }

        return DataSource._orderItems[index];
    }

    public IEnumerable<OrderItem> GetList()
    {
        return DataSource._orderItems.Select(orderItem => orderItem);
    }

    public IEnumerable<OrderItem> GetListItem(int id)
    {
        return DataSource._orderItems.Select(orderItem => orderItem.OrderId == id); //////////////////////////////////////////
    }

    public void Update(OrderItem newOrderItem)
    {
        int index = existOrderItem(newOrderItem.Id);

        if (index != -1)
        {
            DataSource._orderItems[index] = newOrderItem;
        }

        OtherFunctions.exceptionNotFound("order item", newOrderItem.Id);
    }

    private int existOrderItem(int idNum)
    {
        return DataSource._orderItems.FindIndex(orderItem => orderItem.Id == idNum);
    }
}