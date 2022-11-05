using DO;
namespace Dal;

public class DalOrderitem
{
    public int add(OrderItem orderitem)
    {
        orderitem.Id = DataSource.Config.getNum_runOrderItem();
        DataSource.OrderItems[DataSource.Config.counterOrderitem++] = orderitem;
        return orderitem.Id;
    }

    public OrderItem getById(int id)
    {
        foreach (OrderItem orderitem in DataSource.OrderItems)
        {
            if (orderitem.Id == id)
            {
                return orderitem;
            }
        }
        throw new Exception("not found the orderitem");

    }

    public OrderItem getBy_2Id(int pId, int oId)
    {
        foreach (OrderItem orderitem in DataSource.OrderItems)
        {
            if (orderitem.ProductId == pId && orderitem.OrderId == oId)
            {
                return orderitem;
            }
        }
        throw new Exception("not found the orderitem");
    }

    public OrderItem[] getItemArray(int orderId)
    {
        int num;
        num = DataSource.OrderItems.Count(o => o.OrderId == orderId);
        OrderItem[] items = new OrderItem[num];
        int i = 0;
        foreach (OrderItem orderitem in DataSource.OrderItems)
        {
            if (orderitem.OrderId == orderId)
            {
                items[i++] = orderitem;
            }
        }
        return items;
    }

    public OrderItem[] getarry_all()
    {
        OrderItem[] OrderItems = new OrderItem[DataSource.Config.counterOrderitem];
        for (int i = 0; i < DataSource.Config.counterOrderitem; i++)
        {
            OrderItems[i] = DataSource.OrderItems[i];
        }
        return OrderItems;
    }

    public void delete(int id)
    {
        int i = 0;
        for (; i < DataSource.Config.counterOrderitem; i++)
            if (DataSource.OrderItems[i].Id == id)
                break;
        if (i == DataSource.Config.counterOrderitem)
            throw new Exception("not found the orderitem");
        while (i < DataSource.Config.counterOrderitem - 1)
        {
            DataSource.OrderItems[i] = DataSource.OrderItems[i + 1];
            i++;
        }
        DataSource.Config.counterOrderitem--;
    }

    public void update(OrderItem orderitem)
    {
        int i = 0;
        for (; i < DataSource.Config.counterOrderitem; i++)
        {
            if (orderitem.Id == DataSource.OrderItems[i].Id)
            {
                DataSource.OrderItems[i] = orderitem;
                break;
            }
        }
        if (i == DataSource.Config.counterOrderitem)
            throw new Exception("not found the orderitem");
    }

}
