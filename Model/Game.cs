using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
namespace Booklist.Model
{
    public class Game : BaseMedia
    {
        Game() : base()
        {
            Publisher = "";
            Studio = "";
            Series = "";
            Consoles = new string[0];
        }
        [JsonProperty(PropertyName = "publisher")]
        public string Publisher { get; set; }
        [JsonProperty(PropertyName = "studio")]
        public string Studio { get; set; }
        [JsonProperty(PropertyName = "series")]
        public string Series { get; set; }
        [JsonProperty(PropertyName = "consoles")]
        public string[] Consoles { get; set; }
    }
}
