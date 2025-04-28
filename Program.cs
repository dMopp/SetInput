// SetInput.cs
// Kleines C# CLI-Tool, um DDC/CI VCP-Werte an den ersten Monitor zu senden.
// (Speziell Input Select Code 0x60 unterstützt, aber allgemein gehalten.)

using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromPoint(POINT pt, uint dwFlags);

    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, out uint numberOfPhysicalMonitors);

    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool GetPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, uint physicalMonitorArraySize, [Out] PHYSICAL_MONITOR[] physicalMonitorArray);

    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool SetVCPFeature(IntPtr hMonitor, byte vcpCode, uint newValue);

    [StructLayout(LayoutKind.Sequential)]
    private struct PHYSICAL_MONITOR
    {
        public IntPtr hPhysicalMonitor;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szPhysicalMonitorDescription;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("SetInput v1.0 - DDC/CI VCP Control Tool");

        if (args.Length != 2)
        {
            Console.WriteLine("Usage: SetInput.exe <VCP Code> <Value>");
            Console.WriteLine("Example: SetInput.exe 60 15");
            return;
        }

        if (!byte.TryParse(args[0], out byte vcpCode))
        {
            Console.WriteLine("Invalid VCP code.");
            return;
        }

        if (!uint.TryParse(args[1], out uint value))
        {
            Console.WriteLine("Invalid value.");
            return;
        }

        POINT pt = new POINT { X = 1, Y = 1 };
        IntPtr monitorHandle = MonitorFromPoint(pt, 2); // MONITOR_DEFAULTTONEAREST

        if (monitorHandle == IntPtr.Zero)
        {
            Console.WriteLine("No monitor found.");
            return;
        }

        if (!GetNumberOfPhysicalMonitorsFromHMONITOR(monitorHandle, out uint numberOfMonitors) || numberOfMonitors == 0)
        {
            Console.WriteLine("No physical monitors detected.");
            return;
        }

        PHYSICAL_MONITOR[] monitors = new PHYSICAL_MONITOR[numberOfMonitors];
        if (!GetPhysicalMonitorsFromHMONITOR(monitorHandle, numberOfMonitors, monitors))
        {
            Console.WriteLine("Failed to get physical monitor handles.");
            return;
        }

        foreach (var monitor in monitors)
        {
            Console.WriteLine($"Setting VCP code {vcpCode} to {value} on monitor {monitor.szPhysicalMonitorDescription}...");
            if (!SetVCPFeature(monitor.hPhysicalMonitor, vcpCode, value))
            {
                Console.WriteLine($"Failed to set VCP code {vcpCode} on monitor {monitor.szPhysicalMonitorDescription}.");
            }
            else
            {
                Console.WriteLine($"Success.");
            }
        }
    }
}
