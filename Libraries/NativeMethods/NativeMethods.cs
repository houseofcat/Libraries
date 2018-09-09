using System;
using System.Runtime.InteropServices;

namespace HouseofCat.Library
{
    /// <summary>
    /// NativeMethods has all Masrhal/Interop code.
    /// </summary>
    public static class NativeMethods
    {
        /// <summary>
        /// Gets PID using Kerne32.dll.
        /// </summary>
        /// <returns>ProcessId</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GetCurrentProcessId();

        /// <summary>
        /// Get the amount of RAM (in KB) physically installed.
        /// </summary>
        /// <param name="TotalMemoryInKilobytes"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        /// <summary>
        /// Gets the current Thread.
        /// </summary>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern IntPtr GetCurrentThread();

        /// <summary>
        /// Sets the Thread affinity to a logical processor.
        /// </summary>
        /// <param name="hThread"></param>
        /// <param name="dwThreadAffinityMask"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern IntPtr SetThreadAffinityMask(IntPtr hThread, IntPtr dwThreadAffinityMask);
    }
}
