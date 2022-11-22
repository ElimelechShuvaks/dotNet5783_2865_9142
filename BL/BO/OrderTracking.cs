namespace BO;

/// <summary>
/// class of OrderTracking
/// </summary>
public class OrderTracking
{
    /// <summary>
    /// id of Order Tracking
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// status of Order Tracking
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// List of DateTime and status.
    /// </summary>
    public List<Tuple<DateTime,OrderStatus>> tuplesOfDateAndStatus  { get; set; }

    public override string ToString()
    {
        return $@"OrderTracking ID: {Id}
Status:{Status}
";
    }
}
