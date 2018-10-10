using HouseofCat.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static HouseofCat.Library.Utilities.SqlHelpers;

namespace HouseofCat.Library.Utilities
{
    public static class ExceptionHelpers
    {
        private static ThreadSafeRandomNumberGenerator _random = new ThreadSafeRandomNumberGenerator();

        public static Task ThrowsRandomSystemExceptionAsync()
        {
            switch (_random.Next(0, 25))
            {
                case 0: throw new Exception();
                case 1: throw new SystemException();
                case 2: throw new IndexOutOfRangeException();
                case 3: throw new NullReferenceException();
                case 4: throw new AccessViolationException();
                case 5: throw new InvalidOperationException();
                case 6: throw new ArgumentException();
                case 7: throw new ArgumentNullException();
                case 8: throw new ArgumentOutOfRangeException();
                case 9: throw new InvalidCastException();
                case 10: throw new InvalidProgramException();
                case 11: throw new InvalidTimeZoneException();
                case 12: throw new AggregateException(new Exception());
                case 13: throw new AggregateException(new Exception[] { new Exception(), new SystemException() });
                case 14: throw new AggregateException();
                case 15: throw new ExternalException();
                case 16: throw new COMException();
                case 17: throw new SEHException();
                case 18: throw new OutOfMemoryException();
                case 19: throw new BadImageFormatException();
                case 20: throw new DivideByZeroException();
                case 21: throw new DllNotFoundException();
                case 22: throw new DuplicateWaitObjectException();
                case 23: throw new ApplicationException();
                case 24: throw new ArithmeticException();
                case 25: throw new InvalidProgramException();
            }

            return Task.CompletedTask;
        }

        public static Task ThrowsRandomNetworkExceptionAsync()
        {
            switch (_random.Next(0, 12))
            {
                case 0: throw new Exception();
                case 1: throw new AccessViolationException();
                case 2: throw new InvalidOperationException();
                case 3: throw new IOException();
                case 4: throw new SocketException();
                case 5: throw new WebException();
                case 6: throw new PingException("Gremlins threw this exception.");
                case 7: throw new HttpRequestException();
                case 8: throw new HttpListenerException();
                case 10: throw new TimeoutException();
                case 12: throw new COMException();
            }

            return Task.CompletedTask;
        }

        private static int _sqlErrorCount = -1;

        public static async Task ThrowsRandomSqlExceptionAsync()
        {
            // Cache count locally to avoid counting.
            if (_sqlErrorCount > 0) { _sqlErrorCount = SqlErrors.Count; }

            switch (_random.Next(0, 9))
            {
                case 0: throw new Exception();
                case 1: throw new AccessViolationException();
                case 2: throw new InvalidOperationException();
                case 3: throw new IOException();
                case 4: throw new SocketException();
                case 5: throw new WebException();
                case 6: throw new TimeoutException();
                case 7: throw new COMException();
                case 8: await GenerateSqlExceptionAsync(49918); break;
                case 9: await GenerateSqlExceptionAsync(_random.Next(0, _sqlErrorCount)); break;
                default: break;
            }
        }
    }
}
