using MelonLoader;
using MoonriseV2Mod.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace MoonriseUpdater
{
    public class MoonriseLoader : MelonPlugin
    {
        internal static string baseUrl = "bG9jYS5sdA==";
        public const string gitApi = "https://api.github.com/repos/Stoned-Code/Moonrise/releases/latest";

        string modDirectory = Path.Combine(Environment.CurrentDirectory, "Mods", "MoonriseV2.dll");

        internal static string WorkingUrl
        {
            get
            {
                string tempUrl = $"{BaseEncoding.Decoder("aHR0cHM6Ly9tb29ucmlzZS1zYw==")}.{BaseEncoding.Decoder(baseUrl)}";
                WebRequest wr = WebRequest.Create(tempUrl + "/md9fjtnj4dm");
                wr.Timeout = 1500;
                wr.Method = "GET";
                wr.Proxy = null;

                string json = "";

                try
                {
                    WebResponse res = wr.GetResponse();

                    using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                        json = sr.ReadToEnd();
                }

                catch
                {

                }

                JObject jObj = JsonConvert.DeserializeObject(json) as JObject;

                if (jObj != null && (bool)jObj.GetValue("foundBackend"))
                    return BaseEncoding.Encoder(tempUrl);

                for (int i = 1; i < 10; i++)
                {
                    try
                    {
                        tempUrl = $"https://moonrise-sc-{i}.{BaseEncoding.Decoder(baseUrl)}";

                        wr.Abort();
                        wr = WebRequest.Create(tempUrl + "/md9fjtnj4dm");
                        wr.Timeout = 1500;
                        wr.Proxy = null;
                        // MoonriseConsole.Log($"Checking {tempUrl}");
                        WebResponse res = wr.GetResponse();

                        using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                        {
                            json = sr.ReadToEnd();
                            // MoonriseConsole.Log(json);
                        }

                        jObj = JsonConvert.DeserializeObject(json) as JObject;

                        if ((bool)jObj.GetValue("foundBackend"))
                            return BaseEncoding.Encoder(tempUrl);
                    }

                    catch { }
                }

                return "N/A";
            }
        }

        public override void OnApplicationEarlyStart() => ModUpdater.CheckUpdate();

        public override void OnApplicationQuit() => ModUpdater.CheckUpdate();

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
