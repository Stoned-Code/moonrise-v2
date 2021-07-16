using System;
using System.Reflection;

namespace MoonriseV2Mod
{
    public static class TW9vbnJpc2VDb25zb2xl
    {
        internal static bool logsEnabled = true;
        public static string consoleTime
        {
            get
            {
                var hour = DateTime.Now.Hour;
                string trueHour;
                var minute = DateTime.Now.Minute;
                string trueMinute;
                var second = DateTime.Now.Second;
                string trueSecond;
                var milliseconds = DateTime.Now.Millisecond;
                string trueMilliseconds;

                if (hour < 10) trueHour = $"0{hour}";
                else trueHour = hour.ToString();

                if (minute < 10) trueMinute = $"0{minute}";
                else trueMinute = minute.ToString();

                if (second < 10) trueSecond = $"0{second}";
                else trueSecond = second.ToString();

                if (milliseconds < 100) trueMilliseconds = $"0{milliseconds}";
                else trueMilliseconds = milliseconds.ToString();

                string time = $"{trueHour}:{trueMinute}:{trueSecond}.{trueMilliseconds}";

                return time ?? "N/A";
            }
        }

        public static void Log(string message, ConsoleColor textColor = ConsoleColor.White, string modName = null)
        {
            if (!logsEnabled) return;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(consoleTime);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] [");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(modName ?? Assembly.GetExecutingAssembly().GetName().Name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"] ");
            Console.ForegroundColor = textColor;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ErrorLog(string message, ConsoleColor textColor = ConsoleColor.White, string modName = null)
        {
            if (!logsEnabled) return;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(consoleTime);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] [");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{modName ?? Assembly.GetExecutingAssembly().GetName().Name} | Error");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"] ");
            Console.ForegroundColor = textColor;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
