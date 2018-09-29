using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseofCat.Library.Extensions
{
    public static class ConcurrentBagExtensions
    {
        public static void AddRange<T>(this ConcurrentBag<T> bag, IEnumerable<T> list)
        {
            foreach (T element in list)
            {
                bag.Add(element);
            }
        }

        public static void AddRangeInParallel<T>(this ConcurrentBag<T> bag, IEnumerable<T> list)
        {
            Parallel.ForEach(list, element =>
            { bag.Add(element); });
        }
    }
}
