using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Booklist.Model
{
    // this is going to be trade paperbags and hardcovers only so no singular issues
    // you could force it to not be that
    public class Comic : BaseMedia
    {
        public Comic()
        {
            Series = "";
            SeriesNumber = 0;
            Writer = "";
            IssueFrom = 0;
            IssueTo = 0;
        }
        [JsonProperty(PropertyName = "series")]
        public string Series { get; set; }
        [JsonProperty(PropertyName = "seriesNumber")]
        public int SeriesNumber { get; set; }
        [JsonProperty(PropertyName = "writer")]
        public string Writer { get; set; }
        [JsonProperty(PropertyName = "IssueFrom")]
        public int IssueFrom { get; set; }
        [JsonProperty(PropertyName = "IssueTo")]
        public int IssueTo { get; set; }
    }
}
