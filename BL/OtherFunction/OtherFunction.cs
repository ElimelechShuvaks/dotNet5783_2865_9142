using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtherFunction
{
    internal static class OtherFunction
    {
        internal static string GetToStrings<T>(this IEnumerable<T> values)
        {
            return string.Join('\n', values);
        }
    }
}
