using DalApi;
using DO;
using System.Security.Principal;
using System.Xml.Linq;

namespace Dal;

internal class DalXmlOrder : IOrder
{
    string orderPath = @"..\xml\orders.xml";
    string configPath = @"..\xml\config.xml";

    /// <summary>
    /// help function that take an element and return an appropriate order.
    /// </summary>
    static DO.Order? createOrderfromXElement(XElement element)
    {
        Order order = new Order
        {
            Id = Convert.ToInt32(element.Element("Id")!.Value),
            CustomerName = element.Element("CustomerName")!.Value,
            CustomerEmail = element.Element("CustomerEmail")!.Value,
            CustomerAdress = element.Element("CustomerAdress")!.Value,
        };

        var date = element.Element("OrderDate")!.Value;
        order.OrderDate = string.IsNullOrEmpty(date) ? null : Convert.ToDateTime(date);

        date = element.Element("ShipDate")!.Value;
        order.ShipDate = string.IsNullOrEmpty(date) ? null : Convert.ToDateTime(date);

        date = element.Element("DeliveryDate")!.Value;
        order.DeliveryDate = string.IsNullOrEmpty(date) ? null : Convert.ToDateTime(date);

        return order;
    }

    public int Add(DO.Order order)
    {
        int configRunNum = XmlTools.GetConfigNumber(configPath, "num_runOrder");
        XElement root = XmlTools.LoadListFromXElement(orderPath);

        root.Add(new XElement("Order",
                   new XElement("Id", configRunNum),
                   new XElement("CustomerName", order.CustomerName),
                   new XElement("CustomerEmail", order.CustomerEmail),
                   new XElement("CustomerAdress", order.CustomerAdress),
                   new XElement("OrderDate", order.OrderDate),
                   new XElement("ShipDate", order.ShipDate),
                   new XElement("DeliveryDate", order.DeliveryDate)));

        XmlTools.SaveXElementToXelFile(root, orderPath);

        return configRunNum;
    }

    public void Delete(int id)
    {
        (from element in XmlTools.LoadListFromXElement(orderPath).Elements()
         where Convert.ToInt32(element.Element("Id")) == id
         select element).FirstOrDefault()!.Remove();
    }

    public DO.Order Get(Func<DO.Order?, bool>? func)
    {
        try
        {
            return (from element in XmlTools.LoadListFromXElement(orderPath).Elements()
                    let order = createOrderfromXElement(element)
                    where func!(order)
                    select order).FirstOrDefault()!.Value;
        }
        catch (InvalidOperationException)
        {
            throw new EntityNotExistException("There is no order that meets these conditions in the database.");
        }
    }

    public IEnumerable<DO.Order?> GetList(Func<DO.Order?, bool>? func = null)
    {
        if (func == null)
        {
            return from element in XmlTools.LoadListFromXElement(orderPath).Elements()
                   select createOrderfromXElement(element);
        }
        else
        {
            return from element in XmlTools.LoadListFromXElement(orderPath).Elements()
                   let order = createOrderfromXElement(element)
                   where func(order)
                   select order;
        }
    }

    public void Update(DO.Order order)
    {
        XElement root = XmlTools.LoadListFromXElement(orderPath);

        Delete(order.Id);

        root.Add(new XElement("Order",
                    new XElement("Id", order.Id),
                    new XElement("CustomerName", order.CustomerName),
                    new XElement("CustomerEmail", order.CustomerEmail),
                    new XElement("CustomerAdress", order.CustomerAdress),
                    new XElement("OrderDate", order.OrderDate),
                    new XElement("ShipDate", order.ShipDate),
                    new XElement("DeliveryDate", order.DeliveryDate)));

        XmlTools.SaveXElementToXelFile(root, orderPath);
    }
}
