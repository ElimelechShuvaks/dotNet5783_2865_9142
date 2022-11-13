using DO;
namespace Dal;

public class DalOrder
{
    public int add(Order order)
    {
        order.Id = DataSource.Config.Num_runOrder;
        DataSource._orders[DataSource.Config.CounterOrders++] = order;
        return order.Id;
    }
    public Order get(int id)
    {
        foreach (Order order in DataSource._orders)
        {
            if (order.Id == id)
            {
                return order;
            }
        }
        throw new Exception("not found the order");

    }
    public Order[] getArray()
    {
        Order[] orders = new Order[DataSource.Config.CounterOrders];
        for (int i = 0; i < DataSource.Config.CounterOrders; i++)
        {
            orders[i] = DataSource._orders[i];
        }
        return orders;
    }
    public void delete(int id)
    {
        int i = 0;
        for (; i < DataSource.Config.CounterOrders; i++)
            if (DataSource._orders[i].Id == id)
                break;
        if (i == DataSource.Config.CounterOrders)
            throw new Exception("not found the order");
        while (i < DataSource.Config.CounterOrders - 1)
        {
            DataSource._orders[i] = DataSource._orders[i + 1];
            i++;
        }
        DataSource.Config.CounterOrders--;
    }
    public void update(Order order)
    {
        int i = 0;
        for (; i < DataSource.Config.CounterOrders; i++)
        {
            if (order.Id == DataSource._orders[i].Id)
            {
                DataSource._orders[i] = order;
                break;
            }
        }
        if (i == DataSource.Config.CounterOrders)
            throw new Exception("not found the order");
    }
}

