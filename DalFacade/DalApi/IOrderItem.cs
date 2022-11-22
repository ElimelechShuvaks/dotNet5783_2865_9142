using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    OrderItem GetBuy_2Id(int pId, int oId);
    IEnumerable<OrderItem> GetListItem(int id);

    IEnumerable<OrderItem> GetOrderItemsWithPredicate(Predicate<OrderItem> predicate = null);
}
