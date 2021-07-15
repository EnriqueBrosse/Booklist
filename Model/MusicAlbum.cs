using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Booklist.Model
{
    public class MusicAlbum : BaseMedia
    {
        public MusicAlbum()
        {
            Artist = "";
        }
        [JsonProperty(PropertyName = "artist")]
        public string Artist { get; set; }

    }
}
