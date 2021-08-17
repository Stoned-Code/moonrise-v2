using System;
using System.Text;

namespace MoonriseV2Mod.API
{
    public static class VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09
    {
        public static string Ulc1amIyUmxjZz09(string msg)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(msg);
            return Convert.ToBase64String(textBytes);
        }

        public static string UkdWamIyUmxjZz09(string msg)
        {
            try
            {
                byte[] b54Bytes = Convert.FromBase64String(msg);
                return Encoding.UTF8.GetString(b54Bytes);
            }

            catch (Exception ex)
            {
                MoonriseConsole.ErrorLog($"Error Decoding \"{msg}\"\n{ex}");
                return "Error...";
            }
        }

    }
}
