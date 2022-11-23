﻿namespace BO;

/// <summary>
/// class of OrderTracking
/// </summary>
public class OrderTracking
{
    /// <summary>
    /// id of Order Tracking
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    /// status of Order Tracking
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// List of DateTime and Description.
    /// </summary>
    public List<Tuple<DateTime, string>> tuplesOfDateAndDescription { get; set; }

    public override string ToString()
    {
        return $@"OrderTracking ItemId: {OrderId}
Status:{Status}
";
    }
}
