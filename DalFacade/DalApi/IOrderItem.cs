
using DO;
namespace DalApi;

internal interface IOrderItem : ICrud<OrderItem>
{
    public OrderItem getBuy_2Id(int pId, int oId);
    public List<OrderItem> getItemArray(int orderId);
}
