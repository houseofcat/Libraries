using System;
using System.Diagnostics;
using System.Threading.Tasks;
using static HouseofCat.Library.Miscellaneous;
using ANPC = HouseofCat.Models.Enums.AdoNetPerformanceCounters;

namespace HouseofCat.Library.Monitoring
{
    /// <summary>
    /// Class the wraps around accessing PerformanceCounters for SqlConnection.
    /// </summary>
    public class SqlMonitoring : IDisposable
    {
        /// <summary>
        /// The storage location of the Ado.Net Performance Counters.
        /// </summary>
        public PerformanceCounter[] AdoPerformanceCounters { get; set; } = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public SqlMonitoring()
        {
            SetupAdoPerformanceCounters().GetAwaiter().GetResult();
        }

        private async Task SetupAdoPerformanceCounters()
        {
            var instanceName = await GetInstanceName();
            var perfCounterNames = Enum.GetNames(typeof(ANPC));
            AdoPerformanceCounters = new PerformanceCounter[perfCounterNames.Length];

            for (int i = 0; i < perfCounterNames.Length; i++)
            {
                try
                {
                    AdoPerformanceCounters[i] = new PerformanceCounter
                    {
                        CategoryName = ".NET Data Provider for SqlServer",
                        CounterName = perfCounterNames[i],
                        InstanceName = instanceName
                    };
                }
                catch { }
            }
        }

        #region Get Values

        /// <summary>
        /// The total number of connection pools.
        /// </summary>
        /// <returns></returns>
        public Task<float> GetActiveConnectionPoolsCountAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.NumberOfActiveConnectionPools].NextValue());
        }

        /// <summary>
        /// The number of connections that have been reclaimed through garbage collection where 
        /// Close or Dispose was not called by the application. Not explicitly closing or disposing 
        /// connections hurts performance.
        /// </summary>
        /// <returns></returns>
        public Task<float> GetReclaimedConnectionsCountAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.NumberOfReclaimedConnections].NextValue());
        }

        /// <summary>
        /// The number of connections per second that are being made to a database server.
        /// </summary>
        /// <returns></returns>
        public Task<float> GetHardConnectsPerSecondAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.HardConnectsPerSecond].NextValue());
        }

        /// <summary>
        /// The number of disconnects per second that are being made to a database server.
        /// </summary>
        /// <returns></returns>
        public Task<float> GetHardDisconnectsPerSecondAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.HardDisconnectsPerSecond].NextValue());
        }

        /// <summary>
        /// The number of unique connection pool groups that are active. This counter is 
        /// controlled by the number of unique connection strings that are found in the AppDomain.
        /// </summary>
        /// <returns></returns>
        public Task<float> GetActiveConnectionPoolGroupsCountAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.NumberOfActiveConnectionPoolGroups].NextValue());
        }

        /// <summary>
        /// The number of unique connection pool groups that are marked for pruning. This counter is 
        /// controlled by the number of unique connection strings that are found in the AppDomain.
        /// </summary>
        /// <returns></returns>
        public Task<float> GetInactiveConnectionPoolGroupsCountAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.NumberOfInactiveConnectionPoolGroups].NextValue());
        }

        /// <summary>
        /// The number of inactive connection pools that have not had any recent activity and are waiting to
        /// be disposed.
        /// </summary>
        /// <returns></returns>
        public Task<float> GetInactiveConnectionPoolsCountAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.NumberOfInactiveConnectionPools].NextValue());
        }

        /// <summary>
        /// The number of active connections that are not pooled.
        /// </summary>
        /// <returns></returns>
        public Task<float> GetNonPooledConnectionsCountAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.NumberOfNonPooledConnections].NextValue());
        }

        /// <summary>
        /// The number of active connections that are being managed by the connection pooling infrastructure.
        /// </summary>
        /// <returns></returns>
        public Task<float> GetPooledConnectionsCountAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.NumberOfPooledConnections].NextValue());
        }

        /// <summary>
        /// The number of connections currently awaiting completion of an action and which are therefore 
        /// unavailable for use by your application.
        /// </summary>
        /// <returns></returns>
        public Task<float> GetStasisConnectionsCountAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.NumberOfStasisConnections].NextValue());
        }

        /// <summary>
        /// The number of active connections being pulled from the connection pool.
        /// <para>Requires app.config enablement (https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/performance-counters)</para>
        /// </summary>
        /// <returns>PerformanceCounter.NextValue</returns>
        public Task<float> GetSoftConnectsPerSecondAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.SoftConnectsPerSecond].NextValue());
        }

        /// <summary>
        /// The number of active connections that are being returned to the connection pool. 
        /// <para>Requires app.config enablement (https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/performance-counters)</para>
        /// </summary>
        /// <returns>PerformanceCounter.NextValue</returns>
        public Task<float> GetSoftDisconnectsPerSecondAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.SoftDisconnectsPerSecond].NextValue());
        }

        /// <summary>
        /// The number of active connections that are currently in use.
        /// <para>Requires app.config enablement (https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/performance-counters)</para>
        /// </summary>
        /// <returns>PerformanceCounter.NextValue</returns>
        public Task<float> GetActiveConnectionsCountAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.NumberOfActiveConnections].NextValue());
        }

        /// <summary>
        /// The number of connections available for use in the connection pools.
        /// <para>Requires app.config enablement (https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/performance-counters)</para>       
        /// </summary>
        /// <returns>PerformanceCounter.NextValue</returns>
        public Task<float> GetFreeConnectionsCountAsync()
        {
            return Task.FromResult(AdoPerformanceCounters[(int)ANPC.NumberOfFreeConnections].NextValue());
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Just a helpful method of writing all values to console.
        /// </summary>
        public async Task WriteStatsToConsoleAsync()
        {
            var stats = $"\nActive Connection Pool Groups: {await GetActiveConnectionPoolGroupsCountAsync()}"
                + $"\n\rActive Connection Pools: {await GetActiveConnectionPoolsCountAsync()}"
                + $"\n\rActive Connections: {await GetActiveConnectionsCountAsync()}"
                + $"\n\rFree Connections: {await GetFreeConnectionsCountAsync()}"
                + $"\n\rPooled Connections: {await GetPooledConnectionsCountAsync()}"
                + $"\n\rHard Connects Per Second: {await GetHardConnectsPerSecondAsync()}"
                + $"\n\rHard Disconnects Per Second: {await GetHardDisconnectsPerSecondAsync()}"
                + $"\n\rSoft Connects Per Second: {await GetSoftConnectsPerSecondAsync()}"
                + $"\n\rSoft Disconnects Per Second: {await GetSoftDisconnectsPerSecondAsync()}"
                + $"\n\rInactive Connection Pool Groups: {await GetInactiveConnectionPoolGroupsCountAsync()}"
                + $"\n\rInactive Connection Pools: {await GetInactiveConnectionPoolsCountAsync()}"
                + $"\n\rReclaimed Connections: {await GetReclaimedConnectionsCountAsync()}"
                + $"\n\rStasis Connections: {await GetStasisConnectionsCountAsync()}"
                + $"\n\rNon-Pooled Connections: {await GetNonPooledConnectionsCountAsync()}";

            await Console.Out.WriteLineAsync(stats);
        }

        private bool disposedValue = false;

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var item in AdoPerformanceCounters)
                    { item.Dispose(); }

                    // Recreates the objects original state.
                    AdoPerformanceCounters = null;
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        { Dispose(true); }

        #endregion
    }
}
