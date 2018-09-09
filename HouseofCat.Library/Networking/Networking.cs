using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace HouseofCat.Library
{
    public static class NetworkInformation
    {
        public static Task<List<TcpConnectionInformation>> GetActiveTcpConnectionsAsync()
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            var activeTcpConnections = properties.GetActiveTcpConnections().ToList();

            return Task.FromResult(activeTcpConnections);
        }

        public static Task<long> GetActiveTcpConnectionCountAsync()
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            var activeTcpConnections = properties.GetActiveTcpConnections();

            return Task.FromResult(activeTcpConnections.LongLength);
        }

        public static Task<List<IPEndPoint>> GetActiveTcpListenersAsync()
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            var activeTcpListeners = properties.GetActiveTcpListeners().ToList();

            return Task.FromResult(activeTcpListeners);
        }

        public static Task<long> GetActiveTcpListenersCountAsync()
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            var activeTcpListeners = properties.GetActiveTcpListeners();

            return Task.FromResult(activeTcpListeners.LongLength);
        }

        public static Task<TcpStatistics> GetTcpV4StatisticsAsync(bool isIPv6 = false)
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpStatistics tcpStats = null;

            if (isIPv6)
            { tcpStats = properties.GetTcpIPv4Statistics(); }
            else
            { tcpStats = properties.GetTcpIPv4Statistics(); }

            return Task.FromResult(tcpStats);
        }

        public static Task<IcmpV4Statistics> GetIcmpV4StatisticsAsync()
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            var icmpStats = properties.GetIcmpV4Statistics();

            return Task.FromResult(icmpStats);
        }

        public static Task<IcmpV6Statistics> GetIcmpV6StatisticsAsync()
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            var icmpStats = properties.GetIcmpV6Statistics();

            return Task.FromResult(icmpStats);
        }
    }
}
