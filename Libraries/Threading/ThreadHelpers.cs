using System;
using System.Threading.Tasks;

namespace HouseofCat.Library.Threading
{
    /// <summary>
    /// Class for help with dealing with Threads.
    /// </summary>
    public static class ThreadHelpers
    {
        /// <summary>
        /// Uses NativeMethods to set threads affinity to a CPU logical processor. Hardware counts start at 0.
        /// <para>
        /// Example setting value to logical processor 2, single CPU machine, with 4 physical cores and 8 logical processors.
        /// </para>
        /// <para>
        /// (threadPointer, 0, 1, 8)
        /// </para>
        /// </summary>
        /// <param name="threadPointer"></param>
        /// <param name="cpuNumber"></param>
        /// <param name="logicalProcessorNumber"></param>
        /// <param name="logicalProcessorCount"></param>
        /// <returns></returns>
        public static Task SetThreadAffinity(IntPtr threadPointer, int cpuNumber, int logicalProcessorNumber, int logicalProcessorCount)
        {
            var affinityMask = CalculateCoreAffinity(cpuNumber, logicalProcessorNumber, logicalProcessorCount);

            NativeMethods.SetThreadAffinityMask(threadPointer, new IntPtr(affinityMask));

            return Task.CompletedTask;
        }

        /// <summary>
        /// Used for calculating ThreadAffinity. Necessary for assigning a thread to a Core/LP of a CPU.
        /// </summary>
        /// <param name="currentCpu"></param>
        /// <param name="currentCore"></param>
        /// <param name="logicalProcessCount"></param>
        /// <returns></returns>
        public static long CalculateCoreAffinity(int currentCpu, int currentCore, int logicalProcessCount)
        {
            var affinity = 0;

            if (currentCpu == 0)
            { affinity = currentCore; }
            else
            { affinity = currentCore + (int)Math.Pow(logicalProcessCount, currentCpu); }

            return 1L << affinity;
        }
    }
}
