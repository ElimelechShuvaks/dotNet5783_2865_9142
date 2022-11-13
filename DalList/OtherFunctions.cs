using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class OtherFunctions
    {
        internal static void exceptionNotFound(string entityName, int id)
        {
            throw new IdNotExistException($"{entityName} with {id} doesn't exsist in data source");
        }

        internal static void exceptionFound(string entityName, int id)
        {
            throw new IdExistException($"{entityName} with {id} already exsist in data source");
        }
    }
}
