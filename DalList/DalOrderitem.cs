using DalApi;
using DO;

namespace Dal;

internal class DalOrderitem : IOrderItem
{
    public int Add(OrderItem newOrderItem)
    {
        newOrderItem.ItemId = DataSource.Num_runOrderitem;

        DataSource._orderItems.Add(newOrderItem);

        return newOrderItem.ItemId;
    }

    public void Delete(int idNum)
    {
        DataSource._orderItems.Remove(Get(productFunc => productFunc?.ProductId == idNum));
    }

    public void Update(OrderItem newOrderItem)
    {
        int index = existOrderItem(newOrderItem.ItemId);

        if (index != -1)
        {
            DataSource._orderItems[index] = newOrderItem;
            return;
        }

        OtherFunctions.exceptionNotFound("order item", newOrderItem.ItemId);
    }


    private int existOrderItem(int idNum)
    {
        return DataSource._orderItems.FindIndex(orderItem => orderItem?.ItemId == idNum);
    }


    public IEnumerable<OrderItem?> GetList(Func<OrderItem?, bool>? func = null)
    {
        bool check = func is null;
        return check ? DataSource._orderItems.Select(orderItem => orderItem) : DataSource._orderItems.Where(func!);
    }

    public OrderItem Get(Func<OrderItem?, bool>? func)
    {
        OrderItem? orderItem = DataSource._orderItems.FirstOrDefault(func!);
        if (orderItem is not null)
        {
            return (OrderItem)orderItem;
        }
        throw new EntityNotExistException("There is no order item that meets these conditions in the database");
    }
}