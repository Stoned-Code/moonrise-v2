using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonriseUpdater
{
    public static class BaseEncoding
    {
        public static string Encoder(string msg)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(msg);
            return Convert.ToBase64String(textBytes);
        }

        public static string Decoder(string msg)
        {
            try
            {
                byte[] b54Bytes = Convert.FromBase64String(msg);
                return Encoding.UTF8.GetString(b54Bytes);
            }

            catch (Exception ex)
            {
                MoonriseLoader.ErrorLog($"Error Decoding \"{msg}\"\n{ex}");
                return "Error...";
            }
        }
    }
}
