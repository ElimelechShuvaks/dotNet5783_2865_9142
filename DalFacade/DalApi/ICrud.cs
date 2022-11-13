using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface ICrud<T>
{
    int Add(T t);

    T Get(int id);

    void Delete(int id);

    void Update(T t);

    IEnumerable<T> GetList();
}
