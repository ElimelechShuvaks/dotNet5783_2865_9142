using DO;
namespace Dal;

public class DalOrder
{
    public int add(Order order)
    {
        order.Id = DataSource.Config.getNum_runOrder();
        DataSource.Orders[DataSource.Config.counterOrders++] = order;
        return order.Id;
    }
    public Order get(int id)
    {
        foreach (Order order in DataSource.Orders)
        {
            if (order.Id == id)
            {
                return order;
            }
        }
        throw new Exception("not found the order");

    }
    public Order[] getarry()
    {
        Order[] orders = new Order[DataSource.Config.counterOrders];
        for (int i = 0; i < DataSource.Config.counterOrders; i++)
        {
            orders[i] = DataSource.Orders[i];
        }
        return orders;
    }
    public void delete(int id)
    {
        int i = 0;
        for (; i < DataSource.Config.counterOrders; i++)
            if (DataSource.Orders[i].Id == id)
                break;
        if (i == DataSource.Config.counterOrders)
            throw new Exception("not found the order");
        while (i < DataSource.Config.counterOrders - 1)
        {
            DataSource.Orders[i] = DataSource.Orders[i + 1];
            i++;
        }
        DataSource.Config.counterOrders--;
    }
    public void update(Order order)
    {
        int i = 0;
        for (; i < DataSource.Config.counterOrders; i++)
        {
            if (order.Id == DataSource.Orders[i].Id)
            {
                DataSource.Orders[i] = order;
                break;
            }
        }
        if (i == DataSource.Config.counterOrders)
            throw new Exception("not found the order");
    }
}

