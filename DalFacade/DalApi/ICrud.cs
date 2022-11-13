using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface ICrud<T>
{
    public int add(T entity);
    public T get(int id);
    public IEnumerable<T> getArray();
    public void delete(int id);
    public void update(T entity);
}
