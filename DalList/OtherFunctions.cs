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
            throw new IdNotExistException($"{entityName} with id: {id} doesn't exsist in data source");
        }

        internal static void exceptionFound(string entityName, int id)
        {
            throw new IdExistException($"{entityName} with id: {id} already exsist in data source");
        }

        /// <summary>
        /// exeption for get an order item by product id and order id.
        /// </summary>
        /// <param name="entityName">
        /// order iten
        /// </param>
        /// <param name="id1">
        /// produc id
        /// </param>
        /// <param name="id2">
        /// order id
        /// </param>
        /// <exception cref="IdNotExistException"></exception>
        internal static void exceptionNotFound(string entityName, int id1, int id2)
        {
            throw new IdNotExistException($"{entityName} with product id: {id1} and order id: {id2} doesn't exsist in data source");
        }
    }
}
