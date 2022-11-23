using BO;

namespace BlApi;

/// <summary>
/// Interface for order.
/// </summary>
public interface IOrder
{
    /// <summary>
    /// Get OrderForList for manger.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderForList> OrderForListRequest();

    /// <summary>
    /// Get details order.
    /// </summary>
    /// <param name="IdOrder"></param>
    /// <returns></returns>
    public Order GetDetailsOrder(int idOrder);

    /// <summary>
    /// Order shipping update.
    /// </summary>
    /// <param name="IdOrder"></param>
    /// <returns></returns>
    public Order OrderShippingUpdate(int idOrder);

    /// <summary>
    /// Order delivery update.
    /// </summary>
    /// <param name="IdOrder"></param>
    /// <returns></returns>
    public Order OrderDeliveryUpdate(int idOrder);

    /// <summary>
    /// Order Tracking.
    /// </summary>
    /// <param name="IdOrder"></param>
    /// <returns></returns>
    public OrderTracking OrderTracking(int idOrder);


    //the bonus function.

    /// <summary>
    /// option for the maneger to update an order
    /// </summary>
    /// <param name="idOrder"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    public BO.Order OrderUpdate(BO.Order order, int productId, int newAmount);
}
