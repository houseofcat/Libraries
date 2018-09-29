using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseofCat.Library.Extensions
{
    public static class IEnumerableExtensions
    {
        public static Task ParallelForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> funcBody, int maxDoP = 4)
        {
            async Task AwaitPartition(IEnumerator<T> partition)
            {
                using (partition)
                {
                    while (partition.MoveNext())
                    { await funcBody(partition.Current); }
                }
            }

            return Task.WhenAll(
                Partitioner
                    .Create(source)
                    .GetPartitions(maxDoP)
                    .AsParallel()
                    .Select(p => AwaitPartition(p)));
        }

        public static Task ParallelForEachAsync<T1, T2>(this IEnumerable<T1> source, Func<T1, T2, Task> funcBody, T2 inputClass, int maxDoP = 4)
        {
            async Task AwaitPartition(IEnumerator<T1> partition)
            {
                using (partition)
                {
                    while (partition.MoveNext())
                    { await funcBody(partition.Current, inputClass); }
                }
            }

            return Task.WhenAll(
                Partitioner
                    .Create(source)
                    .GetPartitions(maxDoP)
                    .AsParallel()
                    .Select(p => AwaitPartition(p)));
        }

        public static Task ParallelForEachAsync<T1, T2, T3>(this IEnumerable<T1> source, Func<T1, T2, T3, Task> funcBody, T2 inputClass, T3 secondInputClass, int maxDoP = 4)
        {
            async Task AwaitPartition(IEnumerator<T1> partition)
            {
                using (partition)
                {
                    while (partition.MoveNext())
                    { await funcBody(partition.Current, inputClass, secondInputClass); }
                }
            }

            return Task.WhenAll(
                Partitioner
                    .Create(source)
                    .GetPartitions(maxDoP)
                    .AsParallel()
                    .Select(p => AwaitPartition(p)));
        }

        public static Task ParallelForEachAsync<T1, T2, T3, T4>(this IEnumerable<T1> source, Func<T1, T2, T3, T4, Task> funcBody, T2 inputClass, T3 secondInputClass, T4 thirdInputClass, int maxDoP = 4)
        {
            async Task AwaitPartition(IEnumerator<T1> partition)
            {
                using (partition)
                {
                    while (partition.MoveNext())
                    { await funcBody(partition.Current, inputClass, secondInputClass, thirdInputClass); }
                }
            }

            return Task.WhenAll(
                Partitioner
                    .Create(source)
                    .GetPartitions(maxDoP)
                    .AsParallel()
                    .Select(p => AwaitPartition(p)));
        }
    }
}
