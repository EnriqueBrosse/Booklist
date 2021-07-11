using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace Booklist.Model
{
    public class Book : BaseMedia
    {
        public Book() : base()
        {
            Series = "";
            Writer = "";
        }
        [JsonProperty(PropertyName = "series")]
        public string Series { get; set; }
        [JsonProperty(PropertyName = "writer")]
        public string Writer { get; set; }
        [JsonProperty(PropertyName = "seriesNumber")]
        public int SeriesNumber { get; set; }
    }
}
