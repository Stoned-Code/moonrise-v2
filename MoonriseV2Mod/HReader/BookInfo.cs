using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonriseV2Mod.HReader
{
    public class BookInfo
    {
        [JsonProperty] public string hentaiTitle { get; set; }
        [JsonProperty] public string coverUrl { get; set; }
        [JsonProperty] public int pageCount { get; set; }
        [JsonProperty] public string[] pageUrls { get; set; }
        [JsonProperty] public string[] tags { get; set; }
    }
}
