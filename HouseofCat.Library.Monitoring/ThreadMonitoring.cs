using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HouseofCat.Library.Monitoring
{
    /// <summary>
    /// Class that helps monitor Thread usage.
    /// </summary>
    public static class ThreadMonitoring
    {
        /// <summary>
        /// Calculates the number of threads in use by the ThreadPool.
        /// </summary>
        /// <returns>ThreadPool thread count.</returns>
        public static Task<int> GetActiveThreadCountInThreadPoolAsync()
        {
            ThreadPool.GetMaxThreads(out int maxThreads, out int completionPortThreads);
            ThreadPool.GetAvailableThreads(out int availableThreads, out completionPortThreads);

            return Task.FromResult(maxThreads - availableThreads);
        }

        /// <summary>
        /// Calculates the number of completion port threads in use by the ThreadPool.
        /// </summary>
        /// <returns>ThreadPool I/O thread count.</returns>
        public static Task<int> GetActiveCompletionPortThreadCountInThreadPoolAsync()
        {
            ThreadPool.GetMaxThreads(out int maxThreads, out int maxCompletionPortThreads);
            ThreadPool.GetAvailableThreads(out int availableThreads, out int availableCompletionPortThreads);

            return Task.FromResult(maxCompletionPortThreads - availableCompletionPortThreads);
        }

        /// <summary>
        /// Gets the maximum number of threads for the ThreadPool.
        /// </summary>
        /// <returns>Maximum threads in ThreadPool.</returns>
        public static Task<int> GetMaxThreadCountInThreadPoolAsync()
        {
            ThreadPool.GetMaxThreads(out int maxThreads, out int completionPortThreads);

            return Task.FromResult(maxThreads);
        }

        /// <summary>
        /// Gets the maximum number of completion port threads for the ThreadPool.
        /// </summary>
        /// <returns>Maximum completion port threads in ThreadPool.</returns>
        public static Task<int> GetMaxCompletionPortThreadCountInThreadPoolAsync()
        {
            ThreadPool.GetMaxThreads(out int maxThreads, out int maxCompletionPortThreads);

            return Task.FromResult(maxCompletionPortThreads);
        }

        /// <summary>
        /// Gets the available number of threads for the ThreadPool.
        /// </summary>
        /// <returns></returns>
        public static Task<int> GetAvailableThreadCountInThreadPoolAsync()
        {
            ThreadPool.GetAvailableThreads(out int availableThreads, out int completionPortThreads);

            return Task.FromResult(availableThreads);
        }

        /// <summary>
        /// Gets the available number of completion port threads for the ThreadPool.
        /// </summary>
        /// <returns></returns>
        public static Task<int> GetAvailableCompletionPortThreadCountInThreadPoolAsync()
        {
            ThreadPool.GetAvailableThreads(out int availableThreads, out int availableCompletionPortThreads);

            return Task.FromResult(availableCompletionPortThreads);
        }

        /// <summary>
        /// Get the list of all threads currently used by this Process.
        /// </summary>
        /// <returns>Thread count.</returns>
        public static Task<int> GetCurrentProcessThreadCountAsync()
        {
            return Task.FromResult(Process.GetCurrentProcess().Threads.Count);
        }
    }
}
