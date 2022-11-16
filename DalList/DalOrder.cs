using DO;
using DalApi;
namespace Dal;

internal class DalOrder : IOrder
{
    public int Add(Order newOrder)
    {
        newOrder.Id = DataSource.Num_runOrder;
        DataSource._orders.Add(newOrder);
        return newOrder.Id;
    }

    public Order Get(int idNum)
    {
        int index = existOrder(idNum);
        if(index == -1)
        { 
            OtherFunctions.exceptionNotFound("order",idNum);
        }
       return DataSource._orders[index];
    }

    public IEnumerable<Order> GetList()
    {
        return DataSource._orders.Select(order => order);
    }

    public void Delete(int idNum)
    {
        
        Order? order=Get(idNum);

        if(order != null)
        {
            DataSource._orders.Remove(order.Value);
            return;
        }
        OtherFunctions.exceptionNotFound("order",idNum);
    }
    public void Update(Order order)
    {
        int index = existOrder(order.Id);
        if (index != -1)
        {
            DataSource._orders[index] = order;
        }
        OtherFunctions.exceptionNotFound("order", order.Id);
    }

    private int existOrder(int id)
    {
        return DataSource._orders.FindIndex(order => order.Id == id);
    }
}

