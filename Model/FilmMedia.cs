using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
namespace Booklist.Model
{
    public class FilmMedia : BaseMedia
    {
        FilmMedia() : base()
        {
            Series = false;
            Episodes = 0;
            Season = 0;
            Playtime = 0;
            Writer = "";
            Director = "";
        }
        [JsonProperty(PropertyName = "series")]
        public bool Series { get; set; }
        [JsonProperty(PropertyName = "episodes")]
        public int Episodes { get; set; }
        [JsonProperty(PropertyName = "season")]
        public int Season { get; set; }
        [JsonProperty(PropertyName = "playtime")]
        public int Playtime { get; set; }
        [JsonProperty(PropertyName = "writer")]
        public string Writer { get; set; }
        [JsonProperty(PropertyName = "director")]
        public string Director { get; set; }
    }
}
