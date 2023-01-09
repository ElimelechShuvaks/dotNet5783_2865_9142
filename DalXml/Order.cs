using DalApi;
using DO;
using System.Security.Principal;

namespace Dal;

internal class Order : IOrder
{
    public int Add(DO.Order t)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Order Get(Func<DO.Order?, bool>? func)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order?> GetList(Func<DO.Order?, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order t)
    {
        throw new NotImplementedException();
    }
}
