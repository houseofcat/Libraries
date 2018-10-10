using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HouseofCat.Library
{
    public static class Miscellaneous
    {
        /// <summary>
        /// Uses NativeMethods to get the PerformanceCounter Instance name.
        /// </summary>
        /// <returns></returns>
        public static Task<string> GetInstanceName()
        {
            var instanceName = Assembly.GetEntryAssembly().GetName().Name;
            var pid = NativeMethods.GetCurrentProcessId().ToString();

            return Task.FromResult($"{instanceName}[{pid}]");
        }

        /// <summary>
        /// Constructor with parameters for default literal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public static T Construct<T>(params object[] p)
        {
            var ctor = typeof(T)
                .GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(c => c)
                .Where(c => c.GetParameters().Length == p.Length)
                .FirstOrDefault();

            return (T)ctor?.Invoke(p);
        }
    }
}
