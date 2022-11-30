using DO;
using DalApi;
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
        return GetOrderItemsWithPredicate();
    }

    public IEnumerable<OrderItem> GetListItem(int id)
    {
        return DataSource._orderItems.Where(orderItem => orderItem.OrderId == id);
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

    public IEnumerable<OrderItem> GetOrderItemsWithPredicate(Predicate<OrderItem> predicate = null)
    {
        bool check = predicate == null;
        return DataSource._orderItems.Where(orderItem => check ? true : predicate(orderItem));

    }

    private int existOrderItem(int idNum)
    {
        return DataSource._orderItems.FindIndex(orderItem => orderItem.ItemId == idNum);
    }
}