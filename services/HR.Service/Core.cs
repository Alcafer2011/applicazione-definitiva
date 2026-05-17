using System;

namespace HR.Service.Core
{
    public class HRCore
    {
        public static string ServiceName => "HR.Service";
        public static string ServiceVersion => "1.0.0";

        public static void Log(string message)
        {
            Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] {ServiceName}: {message}");
        }
    }
}
