
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    public OrderItem GetBuy_2Id(int pId, int oId);
    public List<OrderItem> GetListItem(int id);
}
