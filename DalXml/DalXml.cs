using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();

    private DalXml() { }

    public IProduct Product { get; } = new Product();

    public IOrder Order { get; } = new Order();

    public IOrderItem OrderItem { get; } = new OrderItem();
}
